using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form : System.Windows.Forms.Form
    {
        int MAX_ITER = 8;
        int cur = -1;
        static double X1 = 20, Y1 = 0, X2 = 20, Y2 = 100;  // start
        public struct Vec
        {

            public double x1, y1, x2, y2;

            public Vec(double x1_, double y1_, double x2_, double y2_)
            {
                this.x1 = x1_;
                this.y1 = y1_;
                this.x2 = x2_;
                this.y2 = y2_;
            }
            public void turn(double alpha) // alpha - rad
            {
                double xn, yn;
                xn = (x2 - x1) * Math.Cos(alpha) - (y2 - y1) * Math.Sin(alpha) + x1;
                yn = (x2 - x1) * Math.Sin(alpha) + (y2 - y1) * Math.Cos(alpha) + y1;
                x2 = xn;
                y2 = yn;
            }
            public void go()  // (x1, y1) -> (x2, y2); (x2, y2) -> (x_new, y_new) | lenght v = const
            {
                double x_new, y_new;
                x_new = x2 + (x2 - x1);
                y_new = y2 + (y2 - y1);
                x1 = x2;
                y1 = y2;
                x2 = x_new;
                y2 = y_new;
            }

            public void to_start()
            {
                x1 = X1;
                x2 = X2;
                y1 = Y1;
                y2 = Y2;
            }
        }

        public Vec v = new Vec(X1, Y1, X2, Y2);
        private void draw_line(Bitmap bmp)
        {
            Pen blackPen = new Pen(Color.Black, 1);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(blackPen, (int) (v.x1 * 10), (int) (v.y1 * 10), (int) (v.x2 * 10), (int) (v.y2 * 10));
                v.go();
            }
        }

        private void turn(int alpha)  // alpha - degree
        {
            v.turn(Math.PI * alpha / 180.0);
        }
        private void line_Koch(Bitmap bmp, int n)
        {
            if (n == 0)
            {
                draw_line(bmp);
                return;
            }
            line_Koch(bmp, n - 1);
            turn(60);
            line_Koch(bmp, n - 1);
            turn(-120);
            line_Koch(bmp, n - 1);
            turn(60);
            line_Koch(bmp, n - 1);
        }

        private void snow(Bitmap bmp, int n)
        {
            line_Koch(bmp, n);
            turn(-120);
            line_Koch(bmp, n);
            turn(-120);
            line_Koch(bmp, n);
            turn(-120);
        }
       
        private void draw(int n)
        {
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            snow(bmp, n);
            pictureBox.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ++cur;
            if (cur <= MAX_ITER)
            {
                Y2 = Y1 + (Y2 - Y1) / 3;
                v.to_start();
                draw(cur);
            }
        }

        public Form()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
