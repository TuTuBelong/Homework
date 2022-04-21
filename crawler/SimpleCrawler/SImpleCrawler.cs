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
        public Hashtable urls = new Hashtable();
        private int count = 0;
       
        public string sites { get; set; }//指定网址
        public string startUrl { get; set; }
        //static void Main(string[] args)
        //{
        //    SimpleCrawler myCrawler = new SimpleCrawler();
        //    string startUrl = "http://www.cnblogs.com/dstang2000/";
        //    myCrawler.sites = "";
        //    if (args.Length >= 1) startUrl = args[0];
        //    myCrawler.urls.Add(startUrl, false);//加入初始页面
        //    new Thread(myCrawler.Crawl).Start();
        //}
        public SimpleCrawler()
        {
             startUrl = "http://www.cnblogs.com/dstang2000/";
             sites = "";
        }
        public void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 10) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html,current);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
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
        private void Parse(string html,string objUrl)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1) .Trim('"', '\"', '#', '>');
                //相对地址转化为绝对地址
                if(!Regex.IsMatch(strRef, @"(http)"))
                {
                    Uri baseUrl = new Uri(objUrl);
                    Uri absoluteUri=new Uri(baseUrl,strRef);
                    strRef = absoluteUri.ToString();
                }
                if (!strRef.Contains(sites)) continue;//只爬取指定网址
                if (strRef.Length == 0) continue;
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
        //判断是否为、html/aspx/jsp等网页
        public void judge(string url)
        {
           
        }
    }
}

