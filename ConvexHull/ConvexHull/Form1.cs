using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvexHull
{
    public partial class Form1 : Form
    {
        List<Point> convexHull;
        bool markRed, paintLines;
        public Form1()
        {
            InitializeComponent();
            panel1.MouseClick += new MouseEventHandler(this.Controll_MouseClike);
            points = new List<Point>();
            convexHull = new List<Point>();

            
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (points.Count < 2)
                return;

            points.Sort(
                delegate(Point a, Point b)
                {
                    int x = a.x - b.x;
                    if (x == 0)
                        return a.y - b.y;
                    return x;
                }
            );

            Stack<Point> hullUpper = new Stack<Point>();
            hullUpper.Push(points[0]);
            hullUpper.Push(points[1]);

            for (int i = 2; i < points.Count; i++)
            {
                hullUpper.Push(points[i]);
                while (hullUpper.Count > 2)
                {
                    Point c = hullUpper.Pop();
                    Point b = hullUpper.Pop();
                    Point a = hullUpper.Pop();

                    if (!Point.isCW(a, b, c))
                    {
                        hullUpper.Push(a);
                        hullUpper.Push(c);
                    }

                    else
                    {
                        hullUpper.Push(a);
                        hullUpper.Push(b);
                        hullUpper.Push(c);
                        break;
                    }
                }
            }

            Stack<Point> hullLower = new Stack<Point>();
            hullLower.Push(points[points.Count - 1]);
            hullLower.Push(points[points.Count - 2]);

            for (int i = points.Count-3; i >= 0; i--)
            {
                hullLower.Push(points[i]);
                while (hullLower.Count > 2)
                {
                    Point c = hullLower.Pop();
                    Point b = hullLower.Pop();
                    Point a = hullLower.Pop();

                    if (!Point.isCW(a, b, c))
                    {
                        hullLower.Push(a);
                        hullLower.Push(c);
                    }

                    else
                    {
                        hullLower.Push(a);
                        hullLower.Push(b);
                        hullLower.Push(c);
                        break;
                    }
                }
            }

            while (hullUpper.Count != 0) convexHull.Add(hullUpper.Pop());
            while (hullLower.Count != 0) convexHull.Add(hullLower.Pop());

            panel1.Refresh();
        }

        private List<Point> points;

        private void Controll_MouseClike(Object sender, MouseEventArgs e)
        {
            points.Add(new Point(e.X, e.Y));
            panel1.Invalidate();
            panel1.Update();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            var g = e.Graphics;

            SolidBrush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Black, 3);

            for (int i = 0; i < points.Count; i++)
            {
               g.DrawRectangle(pen, new Rectangle(points[i].x, points[i].y, 5, 5));
            }
            pen.Color = Color.Red;
            for (int i = 0; i < convexHull.Count - 1; i++)
            {
                g.DrawLine(pen, new PointF(convexHull[i].x, convexHull[i].y), new PointF(convexHull[i + 1].x, convexHull[i + 1].y));
                //g.DrawRectangle(pen, new Rectangle(convexHull[i].x, convexHull[i].y, 5, 5));
               
            }
            if(convexHull.Count > 2)
                g.DrawLine(pen, new PointF(convexHull[0].x, convexHull[0].y), new PointF(convexHull[convexHull.Count - 1].x, convexHull[convexHull.Count - 1].y));

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            points = new List<Point>();
            convexHull = new List<Point>();
            panel1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
