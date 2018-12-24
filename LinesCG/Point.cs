namespace LinesCG
{
    public class Point
    {
        public double x;
        public double y;

        public Point()
        {
        }

        public Point(double a, double b)
        {
            x = a;
            y = b;
        }


        public static Point Coordinate(Point A, double a, double b)
        {
            A.x = a;
            A.y = b;
            return A;
        }
    }
}
