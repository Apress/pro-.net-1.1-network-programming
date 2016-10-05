using System;
using System.Net;
using System.IO;
using System.Text;

class WriteCookie
{
   static void Main(string[] args)
   {
      string query = "http://localhost/postinfo.html";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      string cookie = response.Headers["Set-Cookie"];
if (cookie != null)
{
      int start = cookie.IndexOf("FirstName");
      int end = cookie.IndexOf(';', start);
      int equals = cookie.IndexOf('=', start);

      string value = cookie.Substring(equals + 1, end - equals - 1);
      Console.WriteLine(value);
}
      StreamReader reader = new StreamReader(response.GetResponseStream());
      Console.WriteLine(reader.ReadToEnd());

      response.Close();
   }
}

