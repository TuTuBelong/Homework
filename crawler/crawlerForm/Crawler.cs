using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler
{
  public class SimpleCrawler
  {
    private Hashtable urls = new Hashtable();
    public event Action<SimpleCrawler> CrawlerStopped;
    public event Action<SimpleCrawler, string, string> DownloadAll;
    private Queue<string> queue = new Queue<string>();  // 待下载队列
    private Dictionary<string, bool> Downloaded = new Dictionary<string, bool>();
    public readonly string urlDetectRegex = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";//检测网址
    public static readonly string urlParseRegex = @"^(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
    public string HostFilter { get; set; }
    public string FileFilter { get; set; }
    public int MaxSize { get; set; }
    //网页编码
    public Encoding HtmlEncoding { get; set; }
    private int count = 0;


    public string StartURL { get; set; }

    public SimpleCrawler()
    {
      MaxSize = 50;
      HtmlEncoding = Encoding.UTF8;
    }
    public void Crawl()
    {
      Downloaded.Clear();
      queue.Clear();
      queue.Enqueue(StartURL);
      while (urls.Count <= MaxSize && queue.Count > 0)
      {
        string url = queue.Dequeue();
        try
        {
          string html = DownLoad(url);
          urls[url] = true;
          DownloadAll(this, url, "成功");
          Parse(html, url);

        }
        catch (Exception ex)
        {
          DownloadAll(this, url, "error" + ex.Message);
        }
      }

    }

    public string DownLoad(string url)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Encoding = Encoding.UTF8;
        string html = webClient.DownloadString(url);
        string fileName = count.ToString();
        File.WriteAllText(fileName, html, Encoding.UTF8);
        return html;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return "";
      }
    }
    private void Parse(string html, string objUrl)
    {
      var matches = new Regex(urlDetectRegex).Matches(html);
      foreach (Match match in matches)
      {
        string linkUrl = match.Groups["url"].Value;
        if (linkUrl == null | linkUrl == "") continue;

        if (!Regex.IsMatch(linkUrl, @"(http)"))
        {
          Uri baseUrl = new Uri(objUrl);
          Uri absoluteUri = new Uri(baseUrl, linkUrl);
          linkUrl = absoluteUri.ToString();
        }//转绝对路径
        Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
        string host = linkUrlMatch.Groups["host"].Value;
        string file = linkUrlMatch.Groups["file"].Value;
        if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter) && !Downloaded.ContainsKey(linkUrl))
        {
          queue.Enqueue(linkUrl);
        }


      }
    }

  }
}


