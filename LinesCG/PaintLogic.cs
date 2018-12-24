using System;
using System.Drawing;

namespace LinesCG
{
    public static class PaintLogic
    {
        public static Bitmap bitmap;
        public static Font aFont;
        public static Converter c;
        public static void SetPixel(Graphics g, int x, int y, Color c)
        {
            SolidBrush b = new SolidBrush(c);
            g.FillRectangle(b, x, y, 1, 1);
            b.Dispose();
        }
        public static void SetPixel(Graphics g, int x, int y)
        {
            SetPixel(g, x, y, Color.Red);
            //g.DrawEllipse(Pens.Red, x - 4, y - 4, 8, 8);
        }
        public static void DrawLine(Graphics g, int x1, int y1, int x2, int y2, Color c)
        {
            int sgnx = 1;
            int sgny = 1;
            int x = x1;
            int y = y1;
            int dx = x2 - x1;
            int dy = y2 - y1;
            if (dx < 0)
            {
                sgnx = -1;
                dx = -dx;
            }
            if (dy < 0)
            {
                sgny = -1;
                dy = -dy;
            }
            if (dx > dy)
            {
                int mid = 2 * dy - dx;
                for (int i = 1; i <= dx; i++)
                {
                    SetPixel(g, x, y, c);
                    if (mid < 0)
                    {
                        mid += 2 * dy;
                    }
                    else
                    {
                        SetPixel(g, x, y + 1, c);
                        y += sgny;
                        mid += 2 * dy - 2 * dx;
                    }
                    x += sgnx;
                }
            }
            else
            {
                int mid = 2 * dx - dy;
                for (int i = 1; i <= dy; i++)
                {
                    SetPixel(g, x, y, c);
                    if (mid < 0)
                    {
                        mid += 2 * dx;
                    }
                    else
                    {
                        x += sgnx;
                        mid += 2 * dx - 2 * dy;
                    }
                    y += sgny;
                }
            }
        }
        public static double HH(double a1, double a2)
        {
            double Result = 1;
            while (Math.Abs(a2 - a1) / Result < 1)
                Result /= 10.0;
            while (Math.Abs(a2 - a1) / Result >= 10)
                Result *= 10.0;
            if (Math.Abs(a2 - a1) / Result < 2.0)
                Result /= 5.0;
            if (Math.Abs(a2 - a1) / Result < 5.0)
                Result /= 2.0;
            return Result;
        }

        public static byte GetDigits(double dx)
        {
            byte Result;
            if (dx >= 5) Result = 0;
            else
                if (dx >= 0.5) Result = 1;
            else
                    if (dx >= 0.05) Result = 2;
            else
                        if (dx >= 0.005) Result = 3;
            else
                            if (dx >= 0.0005) Result = 4; else Result = 5;
            return Result;
        }

        public static void OX(Graphics g)
        {
            DrawLine(g, c.II(c.xMin), c.JJ(0), c.II(c.xMax), c.JJ(0), Color.Black);
            double h1 = HH(c.xMin, c.xMax);
            int k1 = (int)Math.Round(c.xMin / h1) - 1;
            int k2 = (int)Math.Round(c.xMax / h1);
            byte Digits = GetDigits(Math.Abs(c.xMax - c.xMin));
            for (int i = k1; i <= k2; i++)
            {
                DrawLine(g, c.II(i * h1), c.JJ(0) - 7, c.II(i * h1), c.JJ(0) + 7, Color.Black);
                if (i != 0)
                    DrawLine(g, c.II(i * h1), c.JJ(c.yMin), c.II(i * h1), c.JJ(c.yMax), Color.Black);
                for (int j = 1; j <= 9; j++)
                    DrawLine(g, c.II(i * h1 + j * h1 / 10), c.JJ(0) - 3, c.II(i * h1 + j * h1 / 10), c.JJ(0) + 3, Color.Black);
                string s = Convert.ToString(Math.Round(h1 * i, Digits));
                g.DrawString(s, aFont, Brushes.Black, c.II(i * h1) - 5, c.JJ(0) + 10);
            }
        }

        public static void OY(Graphics g)
        {
            DrawLine(g, c.II(0), c.JJ(c.yMin), c.II(0), c.JJ(c.yMax), Color.Black);
            double h1 = HH(c.yMin, c.yMax); int k1 = (int)Math.Round(c.yMin / h1) - 1;
            int k2 = (int)Math.Round(c.yMax / h1);
            int Digits = GetDigits(Math.Abs(c.yMax - c.yMin));
            for (int i = k1; i <= k2; i++)
            {
                DrawLine(g, c.II(0) - 7, c.JJ(i * h1), c.II(0) + 7, c.JJ(i * h1), Color.Black);
                if (i != 0)
                    DrawLine(g, c.II(c.xMin), c.JJ(i * h1), c.II(c.xMax), c.JJ(i * h1), Color.Black);
                for (int j = 1; j <= 9; j++)
                    DrawLine(g, c.II(0) - 3, c.JJ(i * h1 + j * h1 / 10), c.II(0) + 3, c.JJ(i * h1 + j * h1 / 10), Color.Black);
                string s = Convert.ToString(Math.Round(h1 * i, Digits));
                g.DrawString(s, aFont, Brushes.Black, c.II(0) + 5, c.JJ(i * h1) - 5);
            }
        }

        public static void Draw(Rectangle r, Line AB, Line DC)
        {
            Point O = new Point();
            Intersection.FindPixel(AB, DC, ref O);
            c.I2 = r.Width;
            c.J2 = r.Height;
            int n = c.I2;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Color cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);

                aFont = new Font("Arial", 11);
                OX(g);
                OY(g);
                double h = (c.xMax - c.xMin) / n;
                for (int i = 1; i < n; i++)
                {
                    DrawLine(g, c.II((int)AB.A.x), c.JJ((int)AB.A.y), c.II((int)AB.B.x), c.JJ((int)AB.B.y), Color.Black);
                    DrawLine(g, c.II((int)DC.A.x), c.JJ((int)DC.A.y), c.II((int)DC.B.x), c.JJ((int)DC.B.y), Color.Black);
                    SetPixel(g, c.II((int)O.x), c.JJ((int)O.y));
                }

            }
        }
    }
}
