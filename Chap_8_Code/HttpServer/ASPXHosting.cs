using System ;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Xml;

public class Host : MarshalByRefObject 
{
   public string HandleRequest(string fileName) 
   {		 
      StringWriter wr = new StringWriter();		 
      Console.WriteLine("The output from the {0} file", fileName);

      // Create a Worker to execute the aspx file
      HttpWorkerRequest worker = new SimpleWorkerRequest(fileName, "" , wr);
      
      // Execute the page
      HttpRuntime.ProcessRequest(worker) ;
      return wr.ToString();
   }
}

public class ASPXHosting
{
   public enum HostInfo{VirtualDirectory, Port}
   public  string CreateHost(string fileName)
   {		 
      Host myHost = (Host)ApplicationHost.CreateApplicationHost(typeof(Host), "/", GetHostingInfo(ASPXHosting.HostInfo.VirtualDirectory));		 
      return myHost.HandleRequest(fileName);
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
}