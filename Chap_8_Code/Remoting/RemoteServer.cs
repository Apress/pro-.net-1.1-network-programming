// RemoteServer.cs
using System;
using APressLog;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http ;

namespace RemoteServer
{
	class RemotingServer
	{
		public static void Main()
		{
			ChannelServices.RegisterChannel(new HttpChannel(8000));		
        
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(RemotingSample), "APressLog",
				WellKnownObjectMode.Singleton);

			Console.WriteLine("Log Server Listening on endpoints:\r\n" +
				"\thttp://localhost:8000/APressLog\r\n");
			Console.WriteLine("Press enter to stop the server...");
			Console.ReadLine();
		}
	}
}
