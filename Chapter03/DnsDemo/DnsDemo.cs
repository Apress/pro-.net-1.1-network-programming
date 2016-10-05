using System;
using System.Net;


	/// <summary>
	/// Demonstrating the use of the Dns class
	/// </summary>
class DnsDemo
{

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	static void Main(string[] args)
	{
		string hostname = "www.microsoft.com";
		IPHostEntry entry = Dns.Resolve(hostname);

		Console.WriteLine("IP Addresses for {0}: ", hostname);
		foreach (IPAddress address in entry.AddressList)
			Console.WriteLine(address.ToString());

		Console.WriteLine("\nAlias names:");
		foreach (string aliasName in entry.Aliases)
			Console.WriteLine(aliasName);

		Console.WriteLine("\nAnd the real hostname:");
		Console.WriteLine(entry.HostName);

	}
}

