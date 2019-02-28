using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptLib;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = Cuneiform.getCuneiformNumber(Convert.ToInt64(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
        }

        private void encryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Replace with File Dialogs
            var from = "C:\\Users\\th1nk\\Dropbox\\YAL\\LastAction.txt";
            var to = "C:\\Users\\th1nk\\Dropbox\\YAL\\LastAction.cuneiform.txt";

            Cuneiform.fileToCuneiform(from, to);

            var result = File.ReadAllText("C:\\Users\\th1nk\\Dropbox\\YAL\\LastAction.cuneiform.txt");
            label1.Text = result;
        }
    }
}
