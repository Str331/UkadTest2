using AngleSharp.Common;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using uk.Extension;
using uk.Helper;

Console.Write("Enter URL-adress: ");
string URL = Console.ReadLine();

URLextension url = new URLextension();
XMLextension xml = new XMLextension();


Console.WriteLine("\t\tURL by crowling");
url.Crawler(URL);
Console.WriteLine("\t\tURL in SiteMap");
xml.URLtoSitemap(URL);