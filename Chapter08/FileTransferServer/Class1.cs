using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;   
using System.Diagnostics;
// using System.Runtime.InteropServices;  
using System.Threading;

public	class fileSender
{
	//file details (Req. for receiver)
	[Serializable]			
	public class FileDetails
	{
		public string FILETYPE="";
		public long FILESIZE=0;

	}private static FileDetails fileDet= new FileDetails();
	
	//UDPClient related vars
	private static IPAddress remoteIPAddress;
	private const int remotePort=5002 ;
	private static UdpClient sender = new UdpClient();	
	private static IPEndPoint endPoint;
	
	//Filestream object
	private static FileStream fs;

	[STAThread]
	static void Main(string[] args)
	{			
		try
		{
			//Get rempte Ip address and create IPEndPoint
			Console.WriteLine("Enter Remote IP address");
			remoteIPAddress=IPAddress.Parse(Console.ReadLine().ToString() );//"127.0.0.1");
			endPoint = new IPEndPoint(remoteIPAddress,remotePort);		
			
			//get file path. (IMP: file size shold be less than 1k)
			Console.WriteLine("Enter File path and name to send.");
			fs  = new FileStream(@Console.ReadLine().ToString() ,FileMode.Open ,FileAccess.Read);

			if (fs.Length > 8192)
			{
				Console.Write ("This version transfers files with size < 8192 bytes"); 
				sender.Close ();
				fs.Close ();
				return;
			}

			SendFileInfo();//Sends file related info to receiver
			
			Thread.Sleep(2000);//Wait for 2 seconds

			SendFile();//File Sending

		}
		catch (Exception eR)
		{
			Console.WriteLine(eR.ToString()); 				
		}
	}
	public static void SendFileInfo()
	{
		
		//get file type or extension 
		fileDet.FILETYPE =fs.Name.Substring((int)fs.Name.Length-3,3);  
		//get file length (Future purpose)
		fileDet.FILESIZE =fs.Length;
				
		XmlSerializer  fileSerializer = new XmlSerializer(typeof(FileDetails));		
		MemoryStream stream = new MemoryStream();
		
		//serialize object
		fileSerializer.Serialize(stream, fileDet);

		//stream to byte
		stream.Position =0;
		Byte [] bytes = new Byte [stream.Length];
		stream.Read(bytes, 0,Convert.ToInt32(stream.Length ));

		Console.WriteLine("Sending file details...");
		//send file details
		sender.Send(bytes, bytes.Length, endPoint);					
		stream.Close();	

	}

	private static void SendFile() 
	{
		//Creating a file stream
		Byte [] bytes = new Byte[fs.Length];
		//stream to bytes
		fs.Read(bytes,0,bytes.Length);
		
		Console.WriteLine ("Sending file...size = " + fs.Length + "bytes");			
		try 
		{				
			sender.Send(bytes, bytes.Length, endPoint);	//send file	
		} 
		catch (Exception eR) 
		{
			Console.WriteLine(eR.ToString());
		}
		finally
		{
			//cleanup
			fs.Close();
			sender.Close();
		}
		Console.WriteLine("File sent suceessfully.");
		Console.Read ();
	}		

}