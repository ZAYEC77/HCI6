using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI6
{
    
    class Book
    {
        [DisplayName("Назва")]
        public string name { get; private set; }
        [DisplayName("Автор")]
        public string author { get; private set; }
        [Browsable(false)]
        public string imageLink { get; private set; }
        [Browsable(false)]
        public string imageLinkLrg { get { return imageLink.Replace("bkt.gif", "lrg.jpg"); } }
        [DisplayName("Дата публікації")]
        public string date { get; private set; }
        [DisplayName("Електронне видання")]
        public string ebook { get; private set; }
        [DisplayName("Друковане видання")]
        public string print { get; private set; }
        [DisplayName("Електронне і друковане видання")]
        public string printEbook { get; private set; }
        public Book(HtmlAgilityPack.HtmlNode node)
        {
            imageLink = node.ChildNodes[3].ChildNodes[1].ChildNodes[0].ChildNodes[0].Attributes[0].Value.ToString();
            name = node.ChildNodes[5].ChildNodes[1].ChildNodes[1].InnerText;
            author = "";
            if (node.ChildNodes[5].ChildNodes[3].ChildNodes.Count == 2)
            {
                author = node.ChildNodes[5].ChildNodes[3].ChildNodes[1].InnerText;
            }
            char[] p = {' ', '\t', '\n'};
            date = node.ChildNodes[5].ChildNodes[5].InnerText.Trim(p);
            ebook = node.ChildNodes[5].ChildNodes[13].ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText.Trim(p).Substring(6);
            if (node.ChildNodes[5].ChildNodes[13].ChildNodes[2].ChildNodes.Count > 0)
            {
                printEbook = node.ChildNodes[5].ChildNodes[13].ChildNodes[2].ChildNodes[0].ChildNodes[1].InnerText.Trim(p).Substring(6);
            }
            if (node.ChildNodes[5].ChildNodes[13].ChildNodes.Count > 4)
            {
                print = node.ChildNodes[5].ChildNodes[13].ChildNodes[5].ChildNodes[0].ChildNodes[1].InnerText.Trim(p).Substring(6);
            }       
        }
    }
}
