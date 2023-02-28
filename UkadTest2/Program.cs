using HtmlAgilityPack;
using System.Diagnostics;
using System.Xml;
using uk.Extension;

Console.WriteLine("Введiть URL-адрес");
string URL = Console.ReadLine();

URLextension url = new URLextension();
XMLextension xml = new XMLextension();

url.Crawler(URL);
Console.WriteLine();
xml.URLtoSitemap(URL);