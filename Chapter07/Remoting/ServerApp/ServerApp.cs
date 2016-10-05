using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemoteSample
{
	public class ServerApp
	{
		public static void Main(string [] arg)
		{
			TcpChannel tcpChannel = new TcpChannel(8080);
			
			ChannelServices.RegisterChannel(tcpChannel);
			
			RemotingConfiguration.RegisterWellKnownServiceType(
			typeof(RemoteSample.ServerObj), "EchoMessage",
			WellKnownObjectMode.SingleCall);			
			
			Console.WriteLine("Hit <enter> to continue...");
			Console.ReadLine();
		}
				
	}
}

