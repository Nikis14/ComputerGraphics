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
        Bitmap[] images = new Bitmap[5];

        int curr_radio = 0;

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

        private Bitmap GenGreyNoMult(Bitmap source)
        {
            Bitmap res = new Bitmap(source.Width,source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    Color c = source.GetPixel(i, i1);
                    int br = (int)Math.Ceiling(0.33 * c.R + 0.33 * c.G + 0.33 * c.B);
                    res.SetPixel(i, i1,Color.FromArgb(br,br,br));
                }
            }
            images[1] = res;
            return res;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            images[0] = new Bitmap(ExecuteFileDialog());
            pictureBox1.Image = images[0];
            GenGreyNoMult(images[0]);
            
        }

        private void radioButton_checkedChanged(object sender,EventArgs e)
        {
            pictureBox1.Image = null;
            if (radioButton1.Checked == true)
            {
                pictureBox1.Image = images[0];
            }
            if (radioButton2.Checked == true)
            {
                pictureBox1.Image = images[1];
            }
        }
    }
}
