using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UkadTest2.Entity;

namespace UkadTest2.Extension
{
    public static class URLextension
    {
        public static bool IsExist(string adress)
        {
            using (var client = new WebClient())
            {
                try
                {
                    using (client.OpenRead(adress)) { return true; }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public static void OrderByResponse(List<URLadress> adressList)
        {
            try
            {
                foreach (var adresses in adressList.OrderBy(x => x.TimeElapse))
                    Console.WriteLine($"{adresses.Name} - Timing(ms): {adresses.TimeElapse}\n");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public static void Print(List<string> adressList)
        {
            try
            {
                foreach (var adresses in adressList)
                    Console.WriteLine(adresses);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public static long ResponseTime(string adress)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(adress);
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                webResponse.Close();
                stopwatch.Stop();

                return stopwatch.ElapsedMilliseconds;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static string URLtoSitemap(string adress) => $"{adress}/sitemap.xml";
    }
}
