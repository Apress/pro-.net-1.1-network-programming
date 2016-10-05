using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Apress.Networking.TCP.FtpUtil
{

   /// <summary>
   /// Summary description for FtpWebResponse.
   /// </summary>
   public class FtpWebResponse : WebResponse
   {
      private FtpWebRequest request;
      private FtpClient client;

      internal FtpWebResponse (FtpWebRequest request)
      {
         this.request = request;
      }

      public override System.IO.Stream GetResponseStream()
      {
         // Split up URI to get hostname and filename
         string hostname;
         string filename;
         GetUriComponents(request.RequestUri.ToString(), out hostname, out filename);

         // Connect to the FTP server and get a stream
         client = new FtpClient(request.Username, request.password);
         client.Connect(hostname);

         NetworkStream dataStream = null;
         switch (request.Method)
         {
            case "GET":
            case "RETR":
               dataStream = client.GetReadStream(filename, request.BinaryMode);
               break;
            case "PUT":
            case "STOR":
               dataStream = client.GetWriteStream(filename, request.BinaryMode);
               break;
            default:
               throw new WebException("Method " + request.Method + " not supported");
         }

         // Create and return and FtpWebStream (to close the underlying objects)
         FtpWebStream ftpStream = new FtpWebStream(dataStream, this);
         return ftpStream;
      }

      private void GetUriComponents(string uri, out string hostname, out string fileName)
      {
         // Check that URI has at least 7 characters, or we'll get an error
         uri = uri.ToLower();
         if (uri.Length < 7)
            throw new UriFormatException("Invalid URI");

         // Check that URI starts "ftp://", and remove that from the start
         if (uri.Substring(0, 6) != "ftp://")
            throw new NotSupportedException("Only FTP requests are supported");
         else
            uri = uri.Substring(6, uri.Length - 6);

         // Divide the rest of the URI into the hostname and the filename
         string[] uriParts = uri.Split(new char[] { '/' }, 2);
         if (uriParts.Length != 2)
            throw new UriFormatException("Invalid URI");

         hostname = uriParts[0];
         fileName = uriParts[1];

      }

      public override void Close()
      {
         client.Close();
      }
   }
}
