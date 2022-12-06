using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace l2mega_informer
{
    public partial class Form_info : Form
    {
        string link;
        public Form_info()
        {
            InitializeComponent();

            webBrowser_info.Navigate(new Uri(ClassMob.info_webBrowser, UriKind.Absolute));
            webBrowser_info.Refresh();
            link = ClassMob.info_linkLabel;
            linkLabel_reva.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void linkLabel_info_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
