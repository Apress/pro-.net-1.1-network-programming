using System;
using System.IO;
using System.Net;
using Apress.Networking.TCP.FtpUtil;


namespace TestClient
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		const int bufferSize = 65536;

		// Upload a file using FtpWebRequest
		public static void UploadDemo()
		{
			FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.251/demofile.bmp");
			//req.Username = "Administrator";
			//req.Password = "secret";
			req.Method = "PUT";	// STOR or PUT
			req.BinaryMode = true;
			Stream writeStream = req.GetResponse().GetResponseStream();

			FileStream fs = new FileStream(@"c:\temp\cool.bmp", FileMode.Open);

			byte[] buffer = new byte[bufferSize];
			int read;
			while ((read = fs.Read(buffer, 0, bufferSize)) > 0)
			{
				writeStream.Write(buffer, 0, bufferSize);
			}
			writeStream.Close();
			req.Close();	
			fs.Close();
		}

		// download a file using FtpWebRequest
		public static void DownloadDemo()
		{
			FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://192.168.0.1/sample.bmp");

			// defaults:
			/*	req.Username = "anonymous";	
				req.Password = "someuser@somemail.com";
				req.BinaryMode = true;	
				req.Method = "GET";		*/

			FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
			Stream stream = resp.GetResponseStream();

			// read a binary file
			FileStream fs = new FileStream(@"c:\temp\sample.bmp", FileMode.Create);
			byte[] buffer = new byte[bufferSize];
			int count;
			do
			{
				Array.Clear(buffer, 0, bufferSize);
				count = stream.Read(buffer, 0, bufferSize);
				fs.Write(buffer, 0, count);
			} while (count > 0);

			stream.Close();
			fs.Close();

			/* read a text file
			StreamReader reader = new StreamReader(stream); 
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				Console.WriteLine(line);
			}
			reader.Close(); */
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// register the ftp schema
			// a config file can be used alternatively
			WebRequest.RegisterPrefix("ftp", new FtpRequestCreator()); 

			//UploadDemo();

			DownloadDemo();



		}
	}
}
