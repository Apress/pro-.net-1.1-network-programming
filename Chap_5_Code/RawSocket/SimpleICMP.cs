using System;
using System.Net;
using System.Net.Sockets ;
namespace RawSocket
{
	// SimpleICMP.cs
	public class SimpleICMP
	{
		public static void Main(string[] args)
		{
			IPHostEntry host = null;
			host = Dns.Resolve("name");
			Socket tmpS = new
				Socket(host.AddressList[0].AddressFamily,
				SocketType.Raw, ProtocolType.Icmp);
		}
	}
}