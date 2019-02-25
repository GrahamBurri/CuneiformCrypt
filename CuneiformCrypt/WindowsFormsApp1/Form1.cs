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
            label1.Text = Cuneiform.getCuneiformNumber(Convert.ToInt16(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
        }

        private void encodeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog read = new OpenFileDialog())
            {
                read.RestoreDirectory = true;

                if (read.ShowDialog() == DialogResult.OK)
                {
                    var frompath = read.FileName;

                    using (SaveFileDialog write = new SaveFileDialog())
                    {
                        write.RestoreDirectory = true;

                        if (write.ShowDialog() == DialogResult.OK)
                        {
                            var topath = write.FileName;
                            Cuneiform.fileToCuneiform(frompath, topath);
                            MessageBox.Show("File " + frompath + " converted to " + topath);
                        }
                    }
                }
            }
        }
    }
}
