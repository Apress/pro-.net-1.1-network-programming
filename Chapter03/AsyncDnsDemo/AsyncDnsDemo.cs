using System;
using System.Net;
class AsyncDnsDemo
{
   private static string hostname = "www.apress.com";
   static void Main(string[] args)
   {
      if (args.Length != 0)
         hostname = args[0];
    //  Dns.BeginGetHostByName(hostname, 
    //     new AsyncCallback(DnsLookupCompleted), null);

      IAsyncResult ar = Dns.BeginGetHostByName(hostname, null, null);
      while (!ar.IsCompleted)
      {
         Console.WriteLine("Can do something else...");
         System.Threading.Thread.Sleep(100);
      }
      DnsLookupCompleted(ar);

      Console.WriteLine("Waiting for the results...");
      Console.ReadLine();
   }

      private static void DnsLookupCompleted(IAsyncResult ar)
      {
         IPHostEntry entry = Dns.EndGetHostByName(ar);
         Console.WriteLine("IP Addresses for {0}: ", hostname);
         foreach (IPAddress address in entry.AddressList)
            Console.WriteLine(address.ToString());
         Console.WriteLine("\nAlias names:");
         foreach (string aliasName in entry.Aliases)
            Console.WriteLine(aliasName);
         Console.WriteLine("\nAnd the real hostname:");
         Console.WriteLine(entry.HostName);
      }
   }
