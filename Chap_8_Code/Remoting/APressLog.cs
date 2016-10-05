// APressLog.cs
using System;

namespace APressLog
{	
	public class RemotingSample : MarshalByRefObject
	{
		public void RemoteLog(string value) 
		{
			Console.WriteLine(value);
		}
	}
}
