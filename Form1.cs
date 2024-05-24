using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FixedRectToolForButano
{
    public partial class Form1 : Form
    {
        int sizeMultiplier = 4;
        Bitmap bmp = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                try
                {
                    bmp = new Bitmap(file);
                    panel1.Width = bmp.Width * sizeMultiplier;
                    panel1.Height = bmp.Height * sizeMultiplier;
                    textBoxImgHeigth.Text = bmp.Width.ToString();
                    panel1.Refresh();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (bmp != null)
            {
                {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    int width = bmp.Width * sizeMultiplier;
                    int height = bmp.Height * sizeMultiplier;
                    e.Graphics.DrawImage(
                        bmp,
                        new Rectangle(0, 0, width, height),  // destination rectangle
                        0,
                        0,           // upper-left corner of source rectangle
                        bmp.Width,       // width of source rectangle
                        bmp.Height,      // height of source rectangle
                        GraphicsUnit.Pixel,
                        null);

                }
                try
                {
                    Brush brush = new HatchBrush(HatchStyle.DottedGrid, Color.Black);
                    Pen pen = new Pen(brush);
                    pen.Width = 1;

                    float imgHeigth = float.Parse(textBoxImgHeigth.Text);
                    float bbCenterX = float.Parse(textBoxCenterX.Text) * sizeMultiplier;
                    float bbCenterY = float.Parse(textBoxCenterY.Text) * sizeMultiplier;
                    float bbWidth = float.Parse(textBoxWidth.Text) * sizeMultiplier;
                    float bbHeight = float.Parse(textBoxHeigth.Text) * sizeMultiplier;
                    float tileIndex = float.Parse(textBoxTileIndex.Text);

                    float centerX = (bmp.Width * sizeMultiplier) / 2f;
                    float centerY = (imgHeigth * sizeMultiplier) / 2f;
                    

                    centerX += bbCenterX;
                    centerY += bbCenterY;

                    float x = centerX - bbWidth / 2f;
                    float y = centerY - bbHeight / 2f + (tileIndex * imgHeigth * sizeMultiplier);



                    e.Graphics.DrawRectangle(pen, x, y, bbWidth, bbHeight);
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
    }
}
