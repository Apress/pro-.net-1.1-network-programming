using System;
using System.Net;
using System.IO;
using System.Text; 

class UploadFile
{
   static void Main(string[] args)
   {
      string siteURL="http://localhost:11000/images/HTTP.txt";
      string remoteResponse;	

      // Create a new WebClient instance.
      WebClient client = new WebClient();

      NetworkCredential cred = new NetworkCredential("Administrator", "OBIWAN", "JULIAN_NEW");

      string fileName = "C:\\HTTP.txt";
      Console.WriteLine("Uploading {0} to {1} ...", fileName, siteURL);

      // File Uploaded using PUT method
      byte[] responseArray = client.UploadFile(siteURL, "PUT", fileName);

      //Response from the Target URL
      remoteResponse = Encoding.ASCII.GetString(responseArray);
      Console.WriteLine(remoteResponse);
   }
}
