using System;
using System.IO;
using System.Net;
using System.Text;

class CurrencyConverter
{
   static void Main()
   {
      HttpWebRequest req;
      HttpWebResponse resp;
      StreamReader sr;

      char[] separator = { ',' };

      string result;
      string fullPath;
      string currencyFrom = "USD";   // US Dollar
      string currencyTo = "INR";     // Indian Rupee
      double amount = 100d;

      Console.WriteLine("Currency Converter");
      Console.WriteLine("Currency From : {0}", currencyFrom);
      Console.WriteLine("Currency To : {0}", currencyTo);
      Console.WriteLine("Amount : {0}", amount);

      // Build the URL that returns the quote
      fullPath = "http://finance.yahoo.com/d/quotes.csv?s=" + currencyFrom +
                 currencyTo + "=X&f=sl1d1t1c1ohgv&e=.csv";
      try
      {
         req = (HttpWebRequest)WebRequest.Create(fullPath);
         resp = (HttpWebResponse)req.GetResponse();     
         sr = new StreamReader(resp.GetResponseStream(), Encoding.ASCII);
         result = sr.ReadLine();
         resp.Close();
         sr.Close();
         string[] temp = result.Split(separator);

         if(temp.Length > 1)
         {
            // Only show the relevant portions
            double rate = Convert.ToDouble(temp[1]);
            double convert = amount * rate;
            Console.WriteLine("{0} {1}(s) = {2} {3}(s)", amount,
                              currencyFrom, convert, currencyTo); 
         }
         else
         {
            Console.WriteLine("Error in getting currency rates " +
                              "from website.");
         }
      }
      catch(Exception e)
      {
         Console.WriteLine("Exception occurred");
      }
   }
}
