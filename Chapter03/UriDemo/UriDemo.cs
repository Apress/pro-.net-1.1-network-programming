using System;



	/// <summary>
	/// Demonstrating the use of the Uri class
	/// </summary>
class UriDemo
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	static void Main(string[] args)
	{
		/*	Uri baseUri = new Uri("http://www.gotdotnet.com");	
			Uri resource1 = new Uri(baseUri, "team/libraries");
			Uri resource2 = new Uri(resource1, "/userarea/default.aspx");
			Console.WriteLine(resource1.AbsoluteUri);
			Console.WriteLine(resource2.AbsoluteUri); */

		Uri resource1 = new Uri("http://www.gotdotnet.com/userarea/default.aspx");
		Uri resource2 = new Uri("http://www.gotdotnet.com/team/libraries/");
		Console.WriteLine(resource1.MakeRelative(resource2));
		Console.WriteLine(resource2.MakeRelative(resource1));

		Uri resource3 = new Uri("http://msdn.microsoft.com/vstudio/default.asp");
		Console.WriteLine(resource2.MakeRelative(resource3));

	}
}

