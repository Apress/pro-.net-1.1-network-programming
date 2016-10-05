using System;
using System.Security;
using System.Security.Permissions;
public class FileIOPDemo2
{
	public static void Main(String[] args)
	{
		FileIOPermissionAccess myPermsAcc  = FileIOPermissionAccess.Read;
		FileIOPermission myPerm = new FileIOPermission(myPermsAcc,   
			@"c:\data.dat ");

		try
		{
			myPerm.Demand();
		}
		catch (SecurityException)
		{
			Console.WriteLine("Sorry, no access.");
		}
	}
}

