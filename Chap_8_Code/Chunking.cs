using System;
using System.IO;
using System.Net;
using System.Text;

class ChunkingExample
{
   static void Main()
   {
      string query = "http://localhost/";
      StreamWriter sw = null;
      string postData = "Posted=true&X=Value";

      HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(query);

      // Setting the request method
      req.Method = "POST";

      // Setting the +-Content-Type header
      req.ContentType = "application/x-www-form-urlencoded";

      // Setting the Content-Length header
      req.ContentLength = postData.Length;

      // Setting the SendChunked property
      req.SendChunked = true;

      // Posting the data
      sw = new StreamWriter(req.GetRequestStream());
      sw.Write(postData);
      sw.Close();

      HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

      StreamReader sr = new StreamReader(resp.GetResponseStream());

      // Reading the output stream
      string outHtml = sr.ReadToEnd();
      Console.WriteLine(outHtml);
      resp.Close();
      sr.Close();
   }
}