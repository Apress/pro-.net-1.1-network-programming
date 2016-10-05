using System;

namespace RawSocket
{
	/// <summary>
	/// Summary description for SendRecRawData.
	/// </summary>
	public class SendRecRawData
	{
		public SendRecRawData()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		static void Main(string[] args)
		{
			// Sets up our socket as previously demonstrated
			IPHostEntry lipa = Dns.Resolve("127.0.0.1");
			IPEndPoint lep = new IPEndPoint(lipa.AddressList[0], 80);
			Socket s = new Socket(lep.Address.AddressFamily,
				SocketType.Raw, ProtocolType.Icmp);
			byte[] msg = Encoding.ASCII.GetBytes("This is a test");
			try
			{
				s.SendTo(msg, 0, msg.Length, SocketFlags.None, lep);
				// Creates an IpEndPoint to capture the identity
				// of the sending host.
				IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				EndPoint tempRemoteEP = (EndPoint)sender;
				// Creates a byte buffer to receive the message.
				byte[] buffer = new byte[1024];
				// Receives datagram from a remote host. This call blocks.
				s.ReceiveFrom(buffer, SocketFlags.None, ref tempRemoteEP);
				// Displays the information received to the screen.
				Console.WriteLine(" I received the following message : "
					+ Encoding.ASCII.GetString(buffer));
				Console.ReadLine();
			}
			catch(Exception e)
			{
				Console.WriteLine("Exception : " + e.ToString());
			}
		}
	}
}
