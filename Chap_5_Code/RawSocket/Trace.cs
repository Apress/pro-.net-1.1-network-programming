using System;

namespace RawSocket
{
	/// <summary>
	/// Summary description for Trace.
	/// </summary>
	public class Trace
	{
		public Trace()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void TraceHost(string host)
		{
			// Declare the IPHostEntry
			IPHostEntry serverHE, fromHE;
			int nBytes = 0;
			int dwStart = 0, dwStop = 0;
			//Initialize a Socket of the Type ICMP
			Socket socket = new
				Socket(AddressFamily.InterNetwork ,SocketType.Raw, ProtocolType.Icmp);
			// Get the server endpoint
			try
			{
				serverHE = Dns.GetHostByName(host);
			}
			catch(Exception)
			{
				Console.WriteLine("Host not found");
				return ;
			}
			// Convert the server IP_EndPoint to an EndPoint
			IPEndPoint ipepServer = new
				IPEndPoint(serverHE.AddressList[0], 0);
			EndPoint epServer = (ipepServer);
			// Set the receiving endpoint to the client machine
			fromHE = Dns.GetHostByName(Dns.GetHostName());
			IPEndPoint ipEndPointFrom = new
				IPEndPoint(fromHE.AddressList[0], 0);
			EndPoint EndPointFrom = (ipEndPointFrom);
			int PacketSize = 0;
			IcmpPacket packet = new IcmpPacket();
			// Construct the packet to send
			packet.Type = 8; //8
			packet.SubCode = 0;
			packet.CheckSum = UInt16.Parse("0");
			packet.Identifier = UInt16.Parse("45");
			packet.SequenceNumber = UInt16.Parse("0");
			int PingData = 32; // sizeof(IcmpPacket) - 8;
			packet.Data = new Byte[PingData];
			// Initialize the Packet.Data
			for (int i = 0; i < PingData; i++)
			{
				packet.Data[i] = (byte)'#';
			}
			// Variable to hold the total packet size
			PacketSize = PingData + 8;
			Byte [] icmp_pkt_buffer = new Byte[ PacketSize ];
			Int32 Index = 0;
			// the total number of bytes in the packet
			Index = Serialize(packet,
				icmp_pkt_buffer,
				PacketSize,
				PingData );
			// Error in packet size
			if( Index == -1 )
			{
				Console.WriteLine("Error in Making Packet");
				return ;
			}
			for(int ittl=1; ittl<= 256; ittl++)
			{
				Byte[] ByteRecv = new Byte[256];
				// Socket options to set TTL and Timeouts
				socket.SetSocketOption(SocketOptionLevel.IP,
					SocketOptionName.IpTimeToLive, ittl);
				socket.SetSocketOption(SocketOptionLevel.Socket,
					SocketOptionName.SendTimeout,10000);
				socket.SetSocketOption(SocketOptionLevel.Socket,
					SocketOptionName.ReceiveTimeout,10000);
				// Get current time
				DateTime dt= DateTime.Now;
				// Send request
				int iRet= socket.SendTo(icmp_pkt_buffer,
					icmp_pkt_buffer.Length, SocketFlags.None,
					epServer);
				// Check for Win32 SOCKET_ERROR
				if(iRet== -1)
					Console.WriteLine("error sending data");
				// Receive
				iRet= socket.ReceiveFrom(ByteRecv, ByteRecv.Length,
					SocketFlags.None, ref EndPointFrom );
				// Calculate time required
				TimeSpan ts= DateTime.Now- dt;
				// Check if response is OK
				if(iRet== -1)
					Console.WriteLine("error getting data");
				Console.WriteLine("TTL= {0,-5} IP= {1,-20}Time={2,3}ms",ittl,((IPEndPoint)epServer)
					.Address,ts.Milliseconds);
				if((iRet == 32+ 8 +20)&&
					(BitConverter.ToInt16(ByteRecv,24) ==
					BitConverter.ToInt16(icmp_pkt_buffer,4))
					&& (ByteRecv[20] == 0))
					break;
				// Time out
				if(ByteRecv[20] != 11)
				{
					Console.WriteLine("Unexpected Error");
					break;
				}
			}
			// Close the socket
			socket.Close();
		}
	}
}
