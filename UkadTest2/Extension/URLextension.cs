using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using uk.Helper;

namespace uk.Extension
{
    public class URLextension
    {
        List<string> UncheckedAdress = new List<string>();
        List<string> CheckedAdress = new List<string>();
        Dictionary<string, long> ResponseTime = new Dictionary<string, long>();

        public void Crawler(string URLadress)
        {
            HTMLhelper helper = new HTMLhelper();
            UncheckedAdress.Add(URLadress);
            while (UncheckedAdress.Count > 0)
            {
                try
                {
                    string value = UncheckedAdress[0];
                    UncheckedAdress.RemoveAt(0);
                    CheckedAdress.Add(value);
                    if (ResponseTime.ContainsKey(value))
                    {
                        Console.WriteLine($"Response for URL: {value} = {ResponseTime[value]} ");
                        continue;
                    }
                    (string, string, long) result = helper.HTMLloader(value);
                    string Page = result.Item1;
                    ResponseTime.Add(result.Item2, result.Item3);
                    Console.WriteLine($"Response for URL: {value} {result.Item3}");

                    var document = new HtmlParser().ParseDocument(Page);
                    var LinkPage = document.QuerySelectorAll("a").Select(x => x.GetAttribute("href")).Where(y => !string.IsNullOrEmpty(y));
                    var SortedLinks = LinkPage.Where(x => x.StartsWith(URLadress) || x.StartsWith('/')).Distinct();

                    foreach(var elements in SortedLinks)
                    {
                        var links = new Uri(new Uri(URLadress), elements).AbsoluteUri;
                        if (links.StartsWith(URLadress) && !UncheckedAdress.Contains(links))
                            UncheckedAdress.Add(links);
                    }
                }
                catch (Exception e)
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
