// RemoteClient.cs
using System;
using System.Runtime.Remoting;
using APressLog;

namespace RemotingClient
{
	class Client
	{
		static void Main()
		{
			RemotingSample httpWroxLog = (RemotingSample)Activator.GetObject(
				typeof(RemotingSample), "http://localhost:8000/APressLog");
			httpWroxLog.RemoteLog("Client : Hello..Server");
		}
	}
}

