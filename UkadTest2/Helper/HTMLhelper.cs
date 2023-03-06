using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uk.Extension;

namespace uk.Helper
{
    public class HTMLhelper
    {
        WebClient webClient;
        public HTMLhelper()
        {
            webClient = new WebClient();
        }
        public (string,string,long) HTMLloader(string adress)
        {
            Stopwatch stopwatch = new Stopwatch();
            long ResponseTime = 0;
            string HTMLpage = null;
            (string, string, long) result;
            try
            {
                stopwatch.Start();
                var response = webClient.DownloadString(adress);
                stopwatch.Stop();
                if (response is not null)
                    HTMLpage = response;

                ResponseTime =stopwatch.ElapsedMilliseconds;
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result = (HTMLpage, adress, ResponseTime);
        }
    }
}
