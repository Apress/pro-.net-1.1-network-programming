using System;
using System.Net;
using System.IO;
using System.Text;

class CookiePersist
{
   static void Main()
   {
      string query = "http://localhost/CookieSample/WriteCookie.aspx";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);

      request.CookieContainer = new CookieContainer();
      // Cookie cookie = new Cookie("", "", "/", "localhost");

      // request.CookieContainer.Add(cookie);

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      CookieCollection cookies = response.Cookies;

      StreamReader reader = new StreamReader(response.GetResponseStream());
      Console.WriteLine(reader.ReadToEnd());
      reader.Close();
      response.Close();

      HttpWebRequest nextRequest = (HttpWebRequest)WebRequest.Create("http://localhost/CookieSample/CookiesTest.aspx");
      nextRequest.CookieContainer = new CookieContainer();
      nextRequest.CookieContainer.Add(cookies);

      HttpWebResponse nextResponse = (HttpWebResponse)nextRequest.GetResponse();
      reader = new StreamReader(nextResponse.GetResponseStream());
      Console.WriteLine(reader.ReadToEnd());
      reader.Close();
      nextResponse.Close();
   }
}

