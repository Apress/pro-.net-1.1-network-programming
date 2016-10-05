using System;
using System.Net;

	/// <summary>
	/// Demonstrating the use of the Uri class
	/// </summary>
class ProxyDemo
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	static void Main(string[] args)
	{
		WebProxy proxy = (WebProxy)GlobalProxySelection.Select;
		Console.WriteLine("Address of the proxy server: {0}", proxy.Address);
		foreach (string bypassAddress in proxy.BypassList)
		{
			Console.WriteLine("Not using the proxy server for this " + 
				"address: {0}", bypassAddress);
		}
		Console.WriteLine ("For local addresses the proxy server is {0}used", 
			proxy.BypassProxyOnLocal ? "not " : "");


	}
}
