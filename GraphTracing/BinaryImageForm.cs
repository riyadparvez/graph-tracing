using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphTracing
{
    public partial class BinaryImageForm : Form
    {
        public BinaryImageForm()
        {
            InitializeComponent();
        }

        private void BinaryImageForm_Load(object sender, EventArgs e)
        {
            Bitmap im = Image.FromFile(@"C:\Users\Riyad\Desktop\Untitled 6.png") as Bitmap;

            Tracer t = new Tracer(im);

            t.Trace();
            binaryImagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            binaryImagePictureBox.Image = t.BinaryImage;

            MessageBox.Show(this, t.ComponentCount.ToString());
        }
    }
}
