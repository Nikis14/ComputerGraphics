using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3Graphics
{
    public partial class Form1 : Form
    {
        Bitmap source;
        int hue_addition = 0;
        int sat_addition = 0;
        int val_addition = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // Color c = Color.fromHS
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void valuebar_Scroll(object sender, EventArgs e)
        {
            val_addition = valuebar.Value;
        }

        private void saturationbar_Scroll(object sender, EventArgs e)
        {
            sat_addition = saturationbar.Value;
        }

        private void huebar_Scroll(object sender, EventArgs e)
        {
            hue_addition = huebar.Value;
        }

        private void openfilebutton_Click(object sender, EventArgs e)
        {
            string s = ExecuteFileDialog();
            source = new Bitmap(ExecuteFileDialog());
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {

        }

        private void savefilebutton_Click(object sender, EventArgs e)
        {

        }

        private string ExecuteFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG";
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
    }
}
