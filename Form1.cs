using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;

namespace HCI6
{
    public partial class Form1 : Form
    {
        int i = -1;
        String url = @"http://shop.oreilly.com/category/bestselling.do";
        List<Book> list = new List<Book>();
        XmlDocument doc = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await parse();
        }

        private async Task parse()
        {
           await RetrievePageAsync(url);
        }

        private async Task RetrievePageAsync(String url)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadStringAsync(new Uri(url));
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(e.Result);
            HtmlAgilityPack.HtmlNodeCollection books = document.DocumentNode.SelectNodes("//td[@class='thumbtext']");
            list.Clear();
            foreach (HtmlAgilityPack.HtmlNode item in books)
            {
                list.Add(new Book(item));
            }
            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            if (i < 0) return;
            pictureBox1.LoadAsync(list[i].imageLink);
        }

        private async void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (i < 0) return;
            pictureBox1.LoadAsync(list[i].imageLink);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            if (i < 0) return;
            pictureBox1.LoadAsync(list[i].imageLink);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (i < 0) return;
            Form f = new Form();
            PictureBox pb = new PictureBox() { Dock = System.Windows.Forms.DockStyle.Fill, SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom };
            f.Controls.Add(pb);
            pb.LoadAsync(list[i].imageLinkLrg);
            f.ShowDialog();
        }

        
    }
}
