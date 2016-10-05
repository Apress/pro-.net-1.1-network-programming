using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Apress.Networking.UDP.ChatApp
{
   class Chat
   {
      private static IPAddress remoteIPAddress;
      private static int remotePort;
      private static int localPort;

      [STAThread]
      static void Main(string[] args)
      {
         try
         {
            // Get necessary data for connection
            Console.WriteLine("Enter Local Port");
            localPort = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Enter Remote Port");         
            remotePort = Convert.ToInt16(Console.ReadLine());         

            Console.WriteLine("Enter Remote IP address");
            remoteIPAddress = IPAddress.Parse(Console.ReadLine());
      
            // Create thread for listening 
            Thread tRec = new Thread(new ThreadStart(Receiver));
            tRec.Start();
      
            while(true)
            {
               Send(Console.ReadLine());               
            }      
         }
         catch (Exception e)
         {
            Console.WriteLine(e.ToString()); 
         }
      }

      private static void Send(string datagram) 
      {
         // Create UdpClient
         UdpClient sender = new UdpClient();

         // Create IPEndPoint with details of remote host
         IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);      

         try 
         {
            // Convert data to byte array
            byte[] bytes = Encoding.ASCII.GetBytes(datagram);

            // Send data
            sender.Send(bytes, bytes.Length, endPoint);            
         } 
         catch (Exception e) 
         {
            Console.WriteLine(e.ToString());
         }
         finally 
         {
            // Close connection
            sender.Close();
         }   
      }

      public static void Receiver()
      {
         // Create a UdpClient for reading incoming data.
         UdpClient receivingUdpClient = new UdpClient(localPort);

         // IPEndPoint with remote host information
         IPEndPoint RemoteIpEndPoint = null; 

         try
         {
            Console.WriteLine(
               "-----------*******Ready for chat!!!*******-----------");

            while(true)
            {
               // Wait for datagram
               byte[] receiveBytes = receivingUdpClient.Receive(
                  ref RemoteIpEndPoint); 

               // Convert and display data
               string returnData = Encoding.ASCII.GetString(receiveBytes);
               Console.WriteLine("-" + returnData.ToString());                                 
            }
         }
         catch (Exception e)
         {
            Console.WriteLine (e.ToString ()); 
         }
      }
   }
}
