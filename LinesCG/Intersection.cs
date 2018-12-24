namespace LinesCG
{
    public class Intersection
    {
        public static void FindPixel(Line AB, Line DC, ref Point O)
        {
            float x, y;
            int ax = (int)AB.A.x;
            int ay = (int)AB.A.y;
            int bx = (int)AB.B.x;
            int by = (int)AB.B.y;
            int cx = (int)DC.A.x;
            int cy = (int)DC.A.y;
            int dx = (int)DC.B.x;
            int dy = (int)DC.B.y;
            if (cx > dx && cy > dy)
            {
                int p1;
                p1 = cx;
                cx = dx;
                dx = p1;
                p1 = cy;
                cy = dy;
                dy = p1;
            }

            float n = (ay - by) * (dx - cx) - (cy - dy) * (bx - ax);
            float m = -((ax * by - bx * ay) * (dx - cx) - (cx * dy - dx * cy) * (bx - ax));
            x = m / n;
            y = ((cy - dy) * (-x) - (cx * dy - dx * cy)) / (dx - cx);

            if (n == 0)
            {
                return;
            }
            else
            {
                if (((ax <= x) && (bx >= x) && (cx <= x) && (dx >= x)) || ((ay <= y) && (by >= y) && (cy <= y) && (dy >= y)) || ((ax >= x) && (bx <= x) && (cx >= x) && (dx <= x)) || ((ay >= y) && (by <= y) && (cy >= y) && (dy <= y)))
                {
                    O = new Point(x, y);
                }
                else
                    return;
            }
        }
    }
}
