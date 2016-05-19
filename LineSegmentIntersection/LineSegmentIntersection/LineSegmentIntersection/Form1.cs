using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineSegmentIntersection
{

   
    public partial class Form1 : Form
    {
        private int clicksCount;
        private Point startPoint;
        private List<Line> linesList;
        private Pen pen;
        private SolidBrush brush;

        public Form1()
        {
            InitializeComponent();
            linesList = new List<Line>();
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Red);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clicksCount % 2 == 0)
            {
                startPoint = new Point(e.X, e.Y);
            }

            else
            {
                linesList.Add(new Line(startPoint, new Point(e.X, e.Y)));
                this.Refresh();
            }

            clicksCount++;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < linesList.Count; i++)
            {
                g.DrawLine(pen, linesList[i].start, linesList[i].end);


                for (int j = i+1; j < linesList.Count; j++)
                {
                    float x11 = linesList[i].start.X;
                    float y11 = linesList[i].start.Y;
                    float x21 = linesList[i].end.X;
                    float y21 = linesList[i].end.Y;

                    float x12 = linesList[j].start.X;
                    float y12 = linesList[j].start.Y;
                    float x22 = linesList[j].end.X;
                    float y22 = linesList[j].end.Y;

                    Point intersectionPoint = Line.get_line_intersection(x11, y11, x21, y21, x12, y12, x22, y22);
                    Console.WriteLine(intersectionPoint.X);
                    Console.WriteLine(intersectionPoint.Y);
                    g.FillRectangle(brush, intersectionPoint.X, intersectionPoint.Y, 3, 3);
                }
            }

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    class Line
    {

        public static  Point UNDIFINED_POINT = new Point((int)-1e5, (int)-1e5);
        public Point start, end;

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }


       public static Point get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y,
    float p2_x, float p2_y, float p3_x, float p3_y)
        {
            float s02_x, s02_y, s10_x, s10_y, s32_x, s32_y, s_numer, t_numer, denom, t;
            s10_x = p1_x - p0_x;
            s10_y = p1_y - p0_y;
            s32_x = p3_x - p2_x;
            s32_y = p3_y - p2_y;

            denom = s10_x * s32_y - s32_x * s10_y;
            if (denom == 0)
                return UNDIFINED_POINT; // Collinear
            bool denomPositive = denom > 0;

            s02_x = p0_x - p2_x;
            s02_y = p0_y - p2_y;
            s_numer = s10_x * s02_y - s10_y * s02_x;
            if ((s_numer < 0) == denomPositive)
                return UNDIFINED_POINT; // No collision

            t_numer = s32_x * s02_y - s32_y * s02_x;
            if ((t_numer < 0) == denomPositive)
                return UNDIFINED_POINT; // No collision

            if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                return UNDIFINED_POINT; // No collision
            // Collision detected
            t = t_numer / denom;
                int i_x = (int)Math.Ceiling(p0_x + (t * s10_x));
                int  i_y = (int) Math.Ceiling(p0_y + (t * s10_y));

                return new Point(i_x, i_y);
        }
    }

}
