using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crawler;
using System.Threading;
using System.Threading.Tasks;

namespace crawlerForm
{
    public partial class Form1 : Form
    {
        public SimpleCrawler myCrawler;
        public Form1()
        {
            InitializeComponent();
            myCrawler = new SimpleCrawler();
            textBox1.DataBindings.Add("Text", myCrawler, "startUrl");
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

        private void button1_Click(object sender, EventArgs e)
        {
            myCrawler.startUrl = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
               myCrawler.urls.Add(myCrawler.startUrl, false);//加入初始页面
               new Thread(myCrawler.Crawl).Start();
        }
    }
}
