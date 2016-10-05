using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemoteSample
{
	public class ClientApp
	{
		public static void Main(string [] arg)
		{
			TcpChannel tcpChannel = new TcpChannel();
			
			ChannelServices.RegisterChannel(tcpChannel);
			
			ServerObj obj = (ServerObj)Activator.GetObject(typeof(RemoteSample.ServerObj)
			,"tcp://localhost:8080/EchoMessage");
			
			Console.WriteLine(obj.ServerMethod("Apress Remoting Sample"));
		}
				
	}
}
