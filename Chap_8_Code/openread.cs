using System;
using System.IO;
using System.Net;

class OpenRead
{
   static void Main(string[] args)
   {
      string siteURL = "http://www.rediff.com";			

      // Create a new WebClient instance.
      WebClient client = new WebClient();

      // Concatenate the domain with the Web resource filename.
      Console.WriteLine("Start Downloading Data From \"{0}\" .......\n\n",
                        siteURL);

      // Download the web resource from the RemoteURL.
      Stream stmData = client.OpenRead(siteURL);			
      StreamReader srData = new StreamReader(stmData);		

      // Create file 
      FileInfo fiData = new FileInfo("C:\\Default.htm");
      StreamWriter st = fiData.CreateText();
      Console.WriteLine("Writing to the file..."); 

      // Write to file
      st.WriteLine(srData.ReadToEnd());				

      st.Close();
      stmData.Close();
   }
}
