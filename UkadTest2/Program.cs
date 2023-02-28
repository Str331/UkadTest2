using HtmlAgilityPack;
using System.Diagnostics;
using System.Xml;
using UkadTest2.Entity;
using UkadTest2.Extension;

Console.WriteLine("Введiть URL-адрес:");
string URLadresses = Console.ReadLine().TrimEnd('/');
List<URLadress> XMLlist = new List<URLadress>();
Stopwatch stopwatch = new Stopwatch();
List<URLadress> URLadressesList = new List<URLadress>();

if (URLextension.IsExist(URLadresses))
{
    HtmlWeb htmlWeb = new HtmlWeb();

    HtmlDocument htmlDocument = htmlWeb.Load(URLadresses);
    HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");

    foreach (HtmlNode node in htmlNodes)
    {
        stopwatch.Start();
        if (node.Attributes["href"].Value.Contains("http"))
        {
            stopwatch.Stop();
            URLadressesList.Add(new URLadress
            {
                Name = node.Attributes["href"].Value.TrimEnd('/'),
                TimeElapse = stopwatch.ElapsedMilliseconds
            });
        }
    }
}
else
{
    Console.WriteLine("URL-адрес не iснує");
    return;
}

XmlDocument xmlDocument = new XmlDocument();

string SiteMapXML = URLextension.URLtoSitemap(URLadresses);
if (URLextension.IsExist(SiteMapXML))
{
    xmlDocument.Load(SiteMapXML);
    foreach (XmlNode xmlNode in xmlDocument.DocumentElement)
    {

        stopwatch.Start();
        if (xmlNode.InnerText.Contains("http"))
        {
            stopwatch.Stop();
            XMLlist.Add(new URLadress
            {
                Name = xmlNode.FirstChild.InnerXml,
                TimeElapse = stopwatch.ElapsedMilliseconds
            });
        }
    }
}
else
{
    Console.WriteLine("Sitemap.xml не iснує");
}

URLadressesList = URLadressesList.GroupBy(x => x.Name).Select(y => y.FirstOrDefault()).ToList();
Console.WriteLine($"Знайденi URLS(html doc): {URLadressesList.Count}\n " +
    $"Знайденi URLS в sitemap: {XMLlist.Count}");
var NameOfURL = URLadressesList.Select(x => x.Name);
var NameOfXML = XMLlist.Select(x => x.Name);

Console.WriteLine("URLS в sitemap,без кроулiнгу");
var OnlyXML = NameOfXML.Except(NameOfURL).ToList();
URLextension.Print(OnlyXML);

Console.WriteLine("URLS знайденi кроулiнгом,без sitemap");
var OnlyURL = NameOfURL.Except(NameOfXML).ToList();
URLextension.Print(OnlyURL);

Console.WriteLine("Вiдсортовано за часом виконання");
var AllbyTiming = XMLlist.Concat(URLadressesList).ToList();
URLextension.OrderByResponse(AllbyTiming);