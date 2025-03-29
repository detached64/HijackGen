using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace HijackGen.GUI
{
    public partial class AboutBox : Form
    {
        private string Link = $"https://github.com/{Application.CompanyName}/{Application.ProductName}";

        public AboutBox()
        {
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            bool is_beta = !string.Equals(Application.ProductVersion.Split('.')[3], "0");
            this.lbName.Text = $"{Application.ProductName} {Application.ProductVersion} {(Environment.Is64BitProcess ? "x64" : "x86")}";
            if (is_beta)
            {
                this.lbName.Text += " Beta";
            }
            this.lbCopyright.Text = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        }

        private void lbSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkSite.LinkVisited = true;
            Process.Start(Link);
        }
    }
}
