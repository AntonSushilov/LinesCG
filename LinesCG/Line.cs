namespace LinesCG
{
    public class Line : Point
    {
        public Point A;
        public Point B;

        public Line()
        {
        }

        public Line(Point A1, Point B1)
        {
            this.A = A1;
            this.B = B1;
        }
    }
}
