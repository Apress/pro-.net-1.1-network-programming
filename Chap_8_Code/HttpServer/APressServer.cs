using System;	
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading ;
using System.Web;
using System.Web.Hosting ;
using System.Xml;

namespace HttpServer
{
   class APressServer : MarshalByRefObject
   {
      // enum for HostInfo
      public enum HostInfo { VirtualDirectory, Port }
      private TcpListener myListener;	

      // The constructor which makes the TcpListener start listening on the
      // given port. It also calls a Thread on the method StartListen(). 
      public APressServer()
      {
         try
         {
            // Start listing on the given port
            myListener = new TcpListener(Int32.Parse(GetHostingInfo(
               HttpServer.APressServer.HostInfo.Port)));
            myListener.Start();
            Console.WriteLine("Web Server Running... Press ^C to Stop...");

            // Start the thread which calls the method 'StartListen'
            Thread thread = new Thread(new ThreadStart(StartListen));
            thread.Start();
         }
         catch (NullReferenceException) 
         {
            // Don't even ask me why they throw this exception
            // when this happens
            Console.WriteLine("Accept failed. Another process might be " +
               "bound to port " +
               HttpServer.APressServer.HostInfo.Port.ToString());
         }
      }

      public string GetHostingInfo(HostInfo InfoType)
      {
         string retVal = "";
         string xPath = "";
         try
         {
            if (InfoType.Equals(HostInfo.VirtualDirectory))
               xPath = "HostLocation/VDir";
            else if (InfoType.Equals(HostInfo.Port))
               xPath = "HostLocation/Port";
            else
               return "";

            XmlDataDocument xDHost = new XmlDataDocument();

            // Loading the XML File
            xDHost.Load("data\\HostInfo.xml");

            XmlNode node = xDHost.SelectSingleNode(xPath);

            retVal = node.InnerText.Trim();
         }
         catch(XmlException  eXML)
         {
            Console.WriteLine("An ConfigFile Exception Occurred : " +
               eXML.ToString());
         }

         return retVal;
      }

      public string GetTheDefaultFileName(string sLocalDirectory)
      {
         string sLine = "";
         try
         {
            XmlDataDocument xDFile = new XmlDataDocument();
            xDFile.Load("data\\Default.xml");

            XmlNodeList fileNodes = xDFile.SelectNodes("Document/File");

            foreach(XmlNode node in fileNodes)
            {
               if (File.Exists(sLocalDirectory + node.InnerText.Trim()))
               {
                  sLine = node.InnerText.Trim();
                  break;
               }
            }
         }
         catch(XmlException  eXML)
         {
            Console.WriteLine("An ConfigFile Exception Occurred : " + eXML.ToString());

         }	
         if (File.Exists( sLocalDirectory + sLine) == true)
         {
            return sLine;
         }
         else
         {
            return "";
         }
      }

      public string GetMimeType(string sRequestedFile)
      {
         string sMimeType = "";
         string sFileExt = "";
         string sMimeExt = "";

         // Convert to lowercase
         sRequestedFile = sRequestedFile.ToLower();			
         int iStartPos = sRequestedFile.IndexOf(".");
         sFileExt = sRequestedFile.Substring(iStartPos);			
         try
         {
            // Loading the Mime.xml to find out the Mime Type
            XmlDataDocument xDMime = new XmlDataDocument();

            // XmlNodeList nodesMime= null;
            xDMime.Load("data\\Mime.xml");
            
            string xPath = "Mime/Values/Type[../Ext='" + sFileExt +"']";
            XmlNode mimeNode = xDMime.SelectSingleNode(xPath);

            if (mimeNode != null)
            {
               sMimeType = mimeNode.InnerText.Trim();
               sMimeExt = mimeNode.PreviousSibling.InnerText.Trim();
            }
         }
         catch (Exception e)
         {
            Console.WriteLine("An Exception Occurred : " + e.ToString());
         }

         if (sMimeExt == sFileExt)
            return sMimeType; 
         else
            return "";
      }

      public void WriteHeader(string sHttpVersion, string sMIMEHeader,
         int iTotalBytes, string sStatusCode,
         ref Socket mySocket)
      {
         string sBuffer = "";	

         // If Mime type is not provided set default to text/html
         if (sMIMEHeader.Length == 0)
            sMIMEHeader = "text/html";

         sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
         sBuffer = sBuffer + "Server: APressServer\r\n";
         sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
         sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
         sBuffer = sBuffer + "Content-Length: " + iTotalBytes + "\r\n\r\n";			
         byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer); 
         SendToBrowser(bSendData, ref mySocket);
         Console.WriteLine("Total Bytes : " + iTotalBytes.ToString());
      }

      public void SendToBrowser(string data, ref Socket socket)
      {
         SendToBrowser(Encoding.ASCII.GetBytes(data), ref socket);
      }

      public void SendToBrowser(Byte[] bSendData, ref Socket socket)
      {
         int iNumByte = 0;			
         try
         {
            if (socket.Connected)
            {
               if (( iNumByte = socket.Send(bSendData, bSendData.Length,0)) == -1)
               {
                  Console.WriteLine("Socket Error cannot Send Packet");
               }
               else
               {
                  Console.WriteLine("No. of bytes send {0}" , iNumByte);
               }
            }
            else
               Console.WriteLine("Connection Dropped....");
         }
         catch (Exception  e)
         {
            Console.WriteLine("Error Occurred : {0} ", e );							
         }
      }

      public void StartListen()
      {
         int iStartPos = 0;
         string sRequest;
         string sDirName;
         string sRequestedFile;
         string sErrorMessage;
         string sLocalDir;

         // Get the virtualDir info
         string sWebServerRoot = GetHostingInfo(HttpServer.APressServer.HostInfo.VirtualDirectory);
         string sPhysicalFilePath = "";
         string sFormattedMessage = "";
         string sResponse = "";
         while(true)
         {
            //Accept a new connection
            Socket socket = myListener.AcceptSocket() ;
            Console.WriteLine ("Socket Type " + socket.SocketType ); 
            if(socket.Connected)
            {
               Console.WriteLine("\nClient Connected!!\n==================\nCLient IP {0}\n", 
                  socket.RemoteEndPoint) ;	

               // Make a byte array and receive data from the client 
               Byte[] bReceive = new Byte[1024] ;
               int i = socket.Receive(bReceive,bReceive.Length,0) ;

               // Convert Byte to string
               string sBuffer = Encoding.ASCII.GetString(bReceive);					

               // Let's just make sure we are using HTTP, that's about all I care about
               iStartPos = sBuffer.IndexOf("HTTP", 1);

               // Get the HTTP text and version e.g. it will return "HTTP/1.1"
               string sHttpVersion = sBuffer.Substring(iStartPos, 8);  				 
               sRequest = sBuffer.Substring(0, iStartPos - 1); 

               // Replace backslash with Forward Slash, if Any
               sRequest.Replace("\\","/");

               // If file name is not supplied add forward slash to indicate 
               // that it is a directory and then we will look for the 
               // default file name..
               if ((sRequest.IndexOf(".") <1) && (!sRequest.EndsWith("/")))
               {
                  sRequest = sRequest + "/"; 
               }

               // Extract the requested file name
               iStartPos = sRequest.LastIndexOf("/") + 1;
               sRequestedFile = sRequest.Substring(iStartPos);					

               // Extract The directory Name
               sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/")-3);										

               // Identify the Physical Directory					
               if (sDirName == "/")
                  sLocalDir = sWebServerRoot;
               else
               {
                  // Get the Virtual Directory
                  sDirName =sDirName.Replace(@"/",@"\");
                  sLocalDir = sWebServerRoot + sDirName;					
               }
               Console.WriteLine("Directory Requested : " +  sLocalDir);

               // Identify the filename
               // If the filename is not supplied, look in the default file list
               if (sRequestedFile.Length == 0 )
               {
                  // Get the default filename
                  sRequestedFile = GetTheDefaultFileName(sLocalDir);
                  if (sRequestedFile == "")						
                  {
                     sErrorMessage = "<H2>Error!! No Default File Name Specified</H2>";
                     WriteHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref socket);
                     SendToBrowser ( sErrorMessage, ref socket);
                     socket.Close();
                     return;
                  }
               }

               // Get TheMime Type					

               string sMimeType = GetMimeType(sRequestedFile);
               //Build the physical path
               sPhysicalFilePath = sLocalDir + "\\" + sRequestedFile;
               Console.WriteLine("File Requested : " +  sPhysicalFilePath);					
               if (File.Exists(sPhysicalFilePath) == false)
               {					
                  sErrorMessage = "<H2>404 Error! File Does Not Exists...</H2>";
                  WriteHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref socket);
                  SendToBrowser( sErrorMessage, ref socket);
                  Console.WriteLine(sFormattedMessage);
               }				
               else
               {	
                  string ucReqFile = sRequestedFile.ToUpper();

                  // If requested file is an ASP.NET page
                  if (ucReqFile.Substring(ucReqFile.Length-4).Equals("ASPX"))
                  {
                     // Create an instance of the ASPXHosting class
                     ASPXHosting host = new ASPXHosting();
                     
                     // Pass in the ASPX file to get HTML output
                     string HTMLOut = host.CreateHost(sRequestedFile);
                     WriteHeader(sHttpVersion, sMimeType, HTMLOut.Length,
                        " 200 OK", ref socket);	
                     SendToBrowser(HTMLOut, ref socket);
                  }
                  else
                  {						
                     int iTotBytes=0;
                     sResponse = "";
                     FileStream fs = new FileStream(sPhysicalFilePath,
                        FileMode.Open, FileAccess.Read, FileShare.Read);
    
                     // Create a reader that can read bytes from the FileStream.						
                     BinaryReader reader = new BinaryReader(fs);
                     byte[] bytes = new byte[fs.Length];
                     int read;
                     while((read = reader.Read(bytes, 0, bytes.Length)) != 0) 
                     {
                        // Read from the file and write the data to the network
                        sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);
                        iTotBytes = iTotBytes + read;
                     }
                     reader.Close(); 
                     fs.Close();
                     WriteHeader(sHttpVersion,  sMimeType, iTotBytes, " 200 OK",
                        ref socket);	
                     SendToBrowser(bytes, ref socket);
                  }	
               }
               socket.Close();
            }
         }
      }

      static void Main()
      {
         APressServer server = new APressServer();
      }
   }
}

