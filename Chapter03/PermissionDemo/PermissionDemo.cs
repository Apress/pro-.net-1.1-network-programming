using System;
using System.Security.Permissions;
using System.Net;
using System.Reflection;

[assembly: WebPermission(SecurityAction.RequestMinimum, 
ConnectPattern="http://www.apress.com")]

[assembly: AssemblyKeyFile("mykey.snk")]

[WebPermission(SecurityAction.Demand, 
	 ConnectPattern="http://www.apress.com")]
class PermissionDemo
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	static void Main(string[] args)
	{


	}
}
