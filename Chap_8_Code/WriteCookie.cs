using System;
using System.Net;
using System.IO;
using System.Text;

class WriteCookie
{
   static void Main(string[] args)
   {
      CookieCollection ccHttp = new CookieCollection();

      // Creating the cookie
      Cookie cHttp = new Cookie("MyName", "Vinod", "/",
                                "localhost");
      string query = "http://localhost/CookieSample/CookiesTest.aspx";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);

      ccHttp.Add(cHttp);

      request.CookieContainer = new CookieContainer();
      request.CookieContainer.Add(ccHttp);

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream(),
                                             Encoding.ASCII);

      Console.WriteLine(reader.ReadToEnd());
      reader.Close();

      Cookie cookie = response.Cookies["NickName"];
      if (cookie != null)
         Console.WriteLine("NickName cookie set with value {0}", cookie.Value);
      else
         Console.WriteLine("NickName cookie not set");

      response.Close();		
   }
}

