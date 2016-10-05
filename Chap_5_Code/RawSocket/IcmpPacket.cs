using System;

namespace RawSocket
{
	/// <summary>
	/// Summary description for IcmpPacket.
	/// </summary>
	public class IcmpPacket
	{
		// Type of message
		public Byte Type;
		// Type of subcode
		public Byte SubCode;
		// One's complement checksum of struct
		public UInt16 CheckSum;
		// Identifier
		public UInt16 Identifier;
		// Sequence number
		public UInt16 SequenceNumber;
		public Byte [] Data;
		public static void Main(string[] args)
		{
			// Here you are passing localhost IP
			// But you can check the ping progam by passing either
			// Internet IP or Network IP
			PingHost("127.0.0.1") ;
		}
		public  IcmpPacket()
		{
		
		}
		/// This method gets the packet and calculates the total size
		/// of the packet by converting it to byte array
		public static Int32 Serialize( IcmpPacket packet,
			Byte [] Buffer, Int32 PacketSize, Int32 PingData )
		{
			Int32 cbReturn = 0;
			// Serialize the struct into the array
			int Index=0;
			Byte [] b_type = new Byte[1];
			b_type[0] = (packet.Type);
			Byte [] b_code = new Byte[1];
			b_code[0] = (packet.SubCode);
			Byte [] b_cksum = BitConverter.GetBytes(packet.CheckSum);
			Byte [] b_id = BitConverter.GetBytes(packet.Identifier);
			Byte [] b_seq = BitConverter.GetBytes(packet.SequenceNumber);
			// Serialize type
			Array.Copy( b_type, 0, Buffer, Index, b_type.Length );
			Index += b_type.Length;
			// Serialize subcode
			Array.Copy( b_code, 0, Buffer, Index, b_code.Length );
			Index += b_code.Length;
			// Serialize cksum
			Array.Copy( b_cksum, 0, Buffer, Index, b_cksum.Length );
			Index += b_cksum.Length;
			// Serialize id
			Array.Copy( b_id, 0, Buffer, Index, b_id.Length );
			Index += b_id.Length;
			Array.Copy( b_seq, 0, Buffer, Index, b_seq.Length );
			Index += b_seq.Length;
			// Copy the data
			Array.Copy( packet.Data, 0, Buffer, Index, PingData );
			Index += PingData;
			if( Index != PacketSize/* sizeof(IcmpPacket) */) 
			{
				cbReturn = -1;
				return cbReturn;
			}
			cbReturn = Index;
			return cbReturn;
		}
		/// This method has the algorithm to make a checksum
		public static UInt16 checksum( UInt16[] buffer, int size )
		{
			Int32 cksum = 0;
			int counter;
			counter = 0;
			while ( size > 0 ) 
			{
				UInt16 val = buffer[counter];
				cksum += Convert.ToInt32( buffer[counter] );
				counter += 1;
				size -= 1;
			}
			cksum = (cksum >> 16) + (cksum & 0xffff);
			cksum += (cksum >> 16);
			return (UInt16)(~cksum);
		}
		/// This method takes the hostname of the server
		/// and then it pings it and shows the response time
		public static void PingHost(string host)
		{
			// Declare the IPHostEntry
			IPHostEntry serverHE, fromHE;
			int nBytes = 0;
			int dwStart = 0, dwStop = 0;
			// Initialize a socket of the type ICMP
			Socket socket = new Socket(AddressFamily.InterNetwork,
				SocketType.Raw, ProtocolType.Icmp);
			// Get the server endpoint
			try
			{
				serverHE = Dns.GetHostByName(host);
			}
			catch(Exception)
			{
				Console.WriteLine("Host not found"); // fail
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
			packet.Type = 8;
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
			// Call the Serialize method you built earlier which
			// counts the total number of bytes in the packet
			Index = Serialize(packet,
				icmp_pkt_buffer,
				PacketSize,
				PingData );
			if( Index == -1 )
			{
				// Error in packet size
				Console.WriteLine("Error in Making Packet");
				return ;
			}
			// Now get this critter into a UInt16 array
			// Get the half size of the packet
			Double double_length = Convert.ToDouble(Index);
			Double dtemp = System.Math.Ceiling( double_length / 2);
			int cksum_buffer_length = Convert.ToInt32(dtemp);
			// Create a byte array
			UInt16 [] cksum_buffer = new UInt16[cksum_buffer_length];
			// Code to initialize the Uint16 array
			int icmp_header_buffer_index = 0;
			for( int i = 0; i < cksum_buffer_length; i++ ) 
			{
				cksum_buffer[i] = BitConverter.ToUInt16
					(icmp_pkt_buffer,icmp_header_buffer_index);
				icmp_header_buffer_index += 2;
			}
			// Call the checksum method you built, which will
			// return a checksum
			UInt16 u_cksum = checksum(cksum_buffer, cksum_buffer_length);
			// Save the checksum to the Packet
			packet.CheckSum = u_cksum;
			// Now that you have the checksum, serialize the packet again
			Byte [] sendbuf = new Byte[ PacketSize ];
			// Again check the packet size
			Index = Serialize(packet,
				sendbuf,
				PacketSize,
				PingData );
			// If there is a error report it
			if( Index == -1 )
			{
				Console.WriteLine("Error in Making Packet");
				return ;
			}
			// Start timing
			dwStart = System.Environment.TickCount;
			// Send the packet over the socket
			try
			{
				if ((nBytes = socket.SendTo(sendbuf,
					PacketSize, 0, epServer)) == -1)
				{
					Console.WriteLine("Socket Error cannot Send Packet");
				}
			}
				// Gracefully handle host unreachable situations
			catch(SocketException ex)
			{
				if(ex.ErrorCode==10065)
					Console.WriteLine("Destination host unreachable.");
			}
			// Initialize the buffers.
			// The receive buffer is the size of the
			// ICMP header plus the IP header (20 bytes)
			Byte [] ReceiveBuffer = new Byte[256];
			nBytes = 0;
			// Receive the bytes
			bool recd =false;
			int timeout=0;
			// Loop for checking the time of the server responding
			while(!recd)
			{
				nBytes = socket.ReceiveFrom
					(ReceiveBuffer, 256, 0, ref EndPointFrom);
				if (nBytes == -1)
				{
					Console.WriteLine("Host not responding") ;
					recd=true ;
					break;
				}
				else if(nBytes>0)
				{
					// Stop timing
					dwStop = System.Environment.TickCount - dwStart;
					Console.WriteLine("Reply from "
						+ epServer.ToString()+" in "
						+ dwStop+"ms :Bytes Received: "+nBytes);
					recd=true;
					break;
				}
				// Time out check
				timeout=System.Environment.TickCount - dwStart;
				if(timeout>1000)
				{
					Console.WriteLine("Time Out") ;
					recd=true;
				}
			}
			// Close the socket
			socket.Close();
		}
	}
}

