using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;

class UploadValues
{
   static void Main(string[] args)
   {
      string siteURL = "http://localhost/Force/SiteContent.aspx";
      string remoteResponse;	

      // Create a new WebClient instance.
      WebClient client = new WebClient();
      NameValueCollection appendURL = new NameValueCollection();

      // Add the NameValueCollection
      appendURL.Add("Type", "14");
      appendURL.Add("Keyword", "WebService");
      Console.WriteLine("Uploading the Value pair");  

      // Upload the NameValueCollection using POST method
      byte[] responseArray = client.UploadValues(siteURL, "POST", appendURL);
      remoteResponse = Encoding.ASCII.GetString(responseArray);
      Console.WriteLine(remoteResponse);
   }
}
