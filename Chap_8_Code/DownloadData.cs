using System;
using System.Net;
using System.Text;

class DownloadData
{
   static void Main()
   {
      WebClient client = new WebClient();
      byte[] urlData = client.DownloadData("http://www.rediff.com");
      string data = Encoding.ASCII.GetString(urlData);
      Console.WriteLine(data);
   }
}