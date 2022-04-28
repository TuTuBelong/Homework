using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crawler;

namespace crawlerForm
{
    public partial class Form1 : Form
    {
    BindingSource resultBindingSource = new BindingSource();
    SimpleCrawler crawler = new SimpleCrawler();
    public Form1()
        {
            InitializeComponent();
           dgvResult.DataSource = resultBindingSource;
           crawler.DownloadAll += Crawler_PageDownloaded;
           crawler.CrawlerStopped += Crawler_CrawlerStopped;
    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultBindingSource.Clear();
            crawler.StartURL = txtUrl.Text;
            Match match = Regex.Match(crawler.StartURL, SimpleCrawler.urlParseRegex);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            crawler.HostFilter = "^" + host + "$";
            crawler.FileFilter = ".(html?|aspx|jsp|php)$|^[^.]*$";
            Thread thread1 = new Thread(() => crawler.Crawl());
            Thread thread2= new Thread(() => crawler.Crawl());
            thread1.IsBackground = true;
            thread1.Start();
            thread2.IsBackground = true;
            thread2.Start();

    }
    private void Crawler_CrawlerStopped(SimpleCrawler obj)
    {
      Action action = () => lblInfo.Text = "爬虫已停止";
      if (this.InvokeRequired)
      {
        this.Invoke(action);
      }
      else
      {
        action();
      }
    }

    private void Crawler_PageDownloaded(SimpleCrawler crawler, string url, string info)
    {
      var pageInfo = new { Index = resultBindingSource.Count + 1, URL = url, Status = info };//绑定表格各个行列
      Action action = () => { resultBindingSource.Add(pageInfo); };
      if (this.InvokeRequired)
      {
        this.Invoke(action);
      }
      else
      {
        action();
      }
    }
    private void button1_Click(object sender, EventArgs e)
        {
               
        }

    private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
