using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1Graphics
{
    public partial class Form1 : Form
    {
        Image source;

        public Form1()
        {
            InitializeComponent();
        }

        private string ExecuteFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG" ;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                 return ofd.FileName;
            }
            else
            {
                return "NONINIT";
            }
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(ExecuteFileDialog());
        }
    }
}
