using System;
using System.Net;
using System.IO;
using System.Text;

class ReadHeaders
{
   static void Main()
   {
      string query = "http://www.amazon.com";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      foreach (string header in response.Headers)
         Console.WriteLine("{0}: {1}", header, response.Headers[header]);

      response.Close();
   }
}

