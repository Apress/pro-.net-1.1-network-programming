using System;
using System.IO;
using System.Net;
using System.Diagnostics; 
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;    

public class fileRecv
{
	//file details 
	[Serializable]
	public class FileDetails
	{
		public string FILETYPE="";
		public long FILESIZE=0;
	}private static FileDetails fileDet;
	
	//UdpClient vars
	private static int localPort=5002 ;	
	private static UdpClient receivingUdpClient = new UdpClient(localPort);	
	private static IPEndPoint RemoteIpEndPoint = null ;

	private static FileStream fs;
	private static Byte[] receiveBytes =new Byte[0];

	
	[STAThread]
	static void Main(string[] args)
	{			
		//Get the file details
		GetFileDetails();
		//Receive file
		ReceiveFile();
	}		
	private static void GetFileDetails()
	{
		try
		{
			Console.WriteLine ("-----------*******Waiting to get File Details!!*******-----------");				
			//receive file info
			receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint); 		
			Console.WriteLine ("----Received File Details!!");				
			
			XmlSerializer fileSerializer = new XmlSerializer(typeof(FileDetails));
			MemoryStream stream1 = new MemoryStream(); 

			//received byte to stream
			stream1.Write(receiveBytes,0,receiveBytes.Length);
			stream1.Position =0; //IMP
			// Call the Deserialize method and cast to the object type.
			fileDet = (FileDetails)fileSerializer.Deserialize(stream1);		
			Console.WriteLine ("Received file of type ." + fileDet.FILETYPE + " whose size is " + fileDet.FILESIZE.ToString () + " bytes"); 	
		}
		catch (Exception eR)
		{
			Console.WriteLine (eR.ToString ()); 
		}		
	}


	public static void ReceiveFile()
	{		
		try
		{
			Console.WriteLine ("-----------*******Waiting to get File!!*******-----------");										
			//receive file
			receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint); 
		
			//Convert and display data
			Console.WriteLine("----File received...Saving...");					
		
			//create temp file from received file extension
			fs = new FileStream("temp." + fileDet.FILETYPE, FileMode.Create, FileAccess.ReadWrite,FileShare.ReadWrite  );
			fs.Write (receiveBytes,0,receiveBytes.Length );					
		
			Console.WriteLine("----File Saved...");

			Console.WriteLine("-------Opening file with associated program------");				
		
			Process.Start (fs.Name); //opens file with associated program			
		}
		catch (Exception eR)
		{
			Console.WriteLine (eR.ToString ()); 
		}
		finally
		{
			fs.Close();
			receivingUdpClient.Close (); 
			Console.Read();
		}
	}
}
