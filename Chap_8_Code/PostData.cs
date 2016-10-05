using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

class PostData
{
   static void Main()
   {
      string SiteURL="http://localhost/postsample.aspx";
      StreamWriter sw = null;

      // Preparing the data to post
      string postData = "Posted=" + HttpUtility.UrlEncode("True") +
                        "&X=" + HttpUtility.UrlEncode("Value");

      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(SiteURL);
      req.Method = "POST";
      req.ContentLength = postData.Length;
      req.ContentType = "application/x-www-form-urlencoded";

      sw = new StreamWriter(req.GetRequestStream());

      // Encoding the data
      byte[] sendBuffer = Encoding.ASCII.GetBytes(postData);

      // Posting the data
      sw.Write(postData);
      sw.Close();

      HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
      StreamReader srData = new StreamReader(resp.GetResponseStream(),
                                             Encoding.ASCII);

      // Reading the output stream
      string outHtml = srData.ReadToEnd();
      Console.WriteLine(outHtml);

      // Close and clean up the StreamReader
      resp.Close();
      srData.Close();
   }
}