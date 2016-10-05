using System;
using System.Net;
using System.IO;
using System.Text; 

class UploadData
{
   static void Main(string[] args)
   {
      string siteURL;
      siteURL = "http://localhost/postsample.aspx";
      WebClient client = new WebClient();
      client.Credentials = System.Net.CredentialCache.DefaultCredentials ;
      string uploadString = "Hello Force..";

      // Adding the HTTP Content-Type Header
      client.Headers.Add("Content-Type",
                         "application/x-www-form-urlencoded");

      // Apply ASCII Encoding to obtain the string as a byte array.
      byte[] sendData = Encoding.ASCII.GetBytes(uploadString);
      Console.WriteLine("Uploading to {0} ...",  siteURL);

      // Upload the string using the POST method.
      byte[] recData = client.UploadData(siteURL, "POST", sendData);

      // Display the response.
      Console.WriteLine("\nResponse received was {0}",
                        Encoding.ASCII.GetString(recData));
   }
}
