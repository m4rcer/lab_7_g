using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab_7_g
{
    public partial class Form1 : Form
    {
        Bitmap pictureBoxBitMap;
        Bitmap spriteBitMap;
        Bitmap cloneBitMap;
        Graphics g_pictureBox;
        Graphics g_sprite;
        int x, y;
        int w, h;
        Timer timer;
        string basePath;
        Image sky;
        Image sea;
        Image sheep;

        public Form1()
        {
            InitializeComponent();
            basePath = Directory.GetCurrentDirectory();
            sky = Image.FromFile($"{basePath}/img/sky.jpg");
            sea = Image.FromFile($"{basePath}/img/sea.png");
            sheep = Image.FromFile($"{basePath}/img/sheep.png");
            this.DoubleBuffered = true;
        }

        private void DrawSprite()
        {
            var rocket = Image.FromFile($"{basePath}/img/rocket.png");
            g_sprite.DrawImage(rocket, 0, 0, w, h);
        }

        void SavePart(int xt, int yt)
        {
            try
            {
                Rectangle cloneRect = new Rectangle(xt, yt, w, h);
                System.Drawing.Imaging.PixelFormat format =
                pictureBoxBitMap.PixelFormat;
                cloneBitMap = pictureBoxBitMap.Clone(cloneRect, format);
            }
            catch(Exception e)
            {

            }
        }

        void DrawBackgroud()
        {
            var g = Graphics.FromImage(pictureBoxBitMap);
            g.DrawImage(sky, new Rectangle(0, 0, 1366, 720));
            g.DrawImage(sheep, new Rectangle(150, 50, 900, 550));
            g.DrawImage(sea, new Rectangle(0, 0, 1366, 720));
        }

        private void drawBtn_Click(object sender, EventArgs e)
        {
            w = 30;
            h = 85;

            x = 665;
            y = 185;

            g_pictureBox = pictureBox.CreateGraphics();
            pictureBoxBitMap = new Bitmap(1366, 720);

            DrawBackgroud();

            pictureBox.Image = pictureBoxBitMap;

            spriteBitMap = new Bitmap(w, h);
            g_sprite = Graphics.FromImage(spriteBitMap);

            DrawSprite();

            cloneBitMap = new Bitmap(w, h);
            SavePart(x, y);

            g_pictureBox.DrawImage(spriteBitMap, x, y);

            pictureBox.Invalidate();


            timer = new Timer();
            timer.Interval = 100;            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (y > pictureBox.Height)
            {
                timer.Stop();
                return;
            }

            g_pictureBox.DrawImage(cloneBitMap, x, y);
            y -= 5;

            SavePart(x, y);
            g_pictureBox.DrawImage(spriteBitMap, x, y);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            var g = pictureBox.CreateGraphics();
            g.Clear(pictureBox.BackColor);
            g.Dispose();
            timer.Tick -= new EventHandler(timer_Tick);
        }
    }
}
