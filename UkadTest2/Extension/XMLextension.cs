using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.Helper;

namespace uk.Extension
{
    public class XMLextension
    {
        private List<string> UrlsFromString(string source, string start, params string[] lst)
        {
            List<string> urls = new List<string>();
            string[] str = source.Split(lst, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in str)
            {
                if (item.StartsWith(start))
                {
                    urls.Add(item.Substring(item.IndexOf('>') + 1));
                }
            }
            return urls;
        }
        List<string> UncheckedAdress = new List<string>();
        List<string> CheckedAdress = new List<string>();
        Dictionary<string, long> ResponseTime = new Dictionary<string, long>();

        public void URLtoSitemap(string URLadress)
        {
            HTMLhelper helper = new HTMLhelper();
            UncheckedAdress.Add(URLadress);
            (string, string, long) result = ("", "", 0);
            while (UncheckedAdress.Count > 0)
            {
                try
                {
                    string value = UncheckedAdress[0];
                    UncheckedAdress.RemoveAt(0);
                    CheckedAdress.Add(value);
                    if (ResponseTime.ContainsKey(value))
                    {
                        Console.WriteLine($"Час вiдповiдi {value} {ResponseTime[value]}");
                        continue;
                    }

                    if (!value.Contains("sitemap.xml"))
                        result = helper.HTMLloader(value + @"/sitemap.xml");
                    else
                        result = helper.HTMLloader(value + @"/sitemap.xml");

                    string Page = result.Item1;
                    ResponseTime.Add(result.Item2, result.Item3);
                    Console.WriteLine($"Час вiдпoвiдi {value} {result.Item3}");

                    if (Page.Contains("<loc>"))
                    {
                        var pageLinks = UrlsFromString(Page, "c>", "<sitemap>", "<url>", "<lo",
                       "<lastmod>", "</sitemap", "</loc>", "</url>", "</lastmod>");
                        var filteredLinks = pageLinks.Where(x => x.StartsWith(URLadress) | x.StartsWith("/")).Distinct();
                        foreach (var elements in filteredLinks)
                        {
                            var links = new Uri(new Uri(URLadress), elements).AbsoluteUri;
                            if (links.StartsWith(URLadress) & !UncheckedAdress.Contains(links) & !CheckedAdress.Contains(links))
                                UncheckedAdress.Add(links);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
            URLadresses = CheckedAdress;
        }
        public List<string> URLadresses { get; set; }
    }
}
