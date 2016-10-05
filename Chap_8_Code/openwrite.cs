using System;
using System.IO;
using System.Net;
using System.Text;

class OpenWrite
{
   static void Main(string[] args)
   {
      string siteURL = "http://localhost/postsample.aspx";

      // Create a new WebClient instance.
      string uploadData = "Posted=True&X=Value";

      // Apply ASCII encoding to obtain an array of bytes .
      byte[] uploadArray = Encoding.ASCII.GetBytes(uploadData);

      // Create a new WebClient instance.
      WebClient client = new WebClient();

      // NetworkCredential cred = new NetworkCredential("Administrator", "OBIWAN", "JULIAN_NEW.APress.com");

      // client.Credentials = cred;
      Console.WriteLine("Uploading data to {0}...", siteURL);
      Stream stmUpload = client.OpenWrite(siteURL, "POST");
      stmUpload.Write(uploadArray, 0, uploadArray.Length);

      // Close the stream and release resources.
      stmUpload.Close();
      Console.WriteLine("Successfully posted the data.");
   }
}

