using System;
using System.Security;
using System.Security.Permissions;
public class FileIOPDemo
{
	public static void Main(String[] args)
	{
		FileIOPermission myPerm = new 	FileIOPermission(FileIOPermissionAccess.AllAccess, 
			@"c:\Networking\Authentication\codetemp");

		SecurityElement mySec = myPerm.ToXml();

		Console.WriteLine(mySec.ToString());
	}
}

