using System;
using System.Net;
using System.IO; 

class DownloadFile
{
   static void Main(string[] args)
   {
      string siteURL = "http://www.dotnetforce.com/images/logo11.gif";
      string fileName = "C:\\ASP.gif";

      // Create a new WebClient instance.
      WebClient client = new WebClient();

      // Concatenate the domain with the Web resource filename.		
      Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n",
                        fileName, siteURL);

      // Download the Web resource and save it into the
      // current filesystem folder.
      client.DownloadFile(siteURL,fileName);    

      Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", 
                        fileName, siteURL);
      Console.WriteLine("\nDownloaded file saved in the following " +
                        "file system folder:\n\t" + fileName);
   }
}
