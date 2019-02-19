using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender,EventArgs e) {
            string link = textBox1.Text;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //  startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl " + link;
            process.StartInfo = startInfo;
            process.Start();

        }
        Boolean check = true;
        string html;
        private void Download2_Click(object sender,EventArgs e) {
            check = true;
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK) {
                html = File.ReadAllText(o.FileName);
                Thread t = new Thread(threadDow);
                t.Start();
            }
        }
        void threadDow() {
            string regex = txtRegex.Text;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Regex reg = new Regex(regex);
            int count = 0;
            Match match = reg.Match(html);
            do {
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C youtube-dl https://www.facebook.com" + match.ToString();
                process.StartInfo = startInfo;
                process.Start();
                MethodInvoker inv = delegate {
                    this.status.Text = count + "/" + match.Length;
                };
                this.Invoke(inv);
                process.WaitForExit();
                match = match.NextMatch();
                count++;
            } while (match != Match.Empty && check == true);
        }

        private void Stop_Click(object sender,EventArgs e) {
            check = false;
        }
    }
}
