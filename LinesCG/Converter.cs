namespace LinesCG
{
    public class Converter
    {
        public int I1 = 0, J1 = 0, I2, J2;
        public double xMin = -10, yMin = -10, xMax = 10, yMax = 10;
        public int II(double x)
        {
            return I1 + (int)((x - xMin) * (I2 - I1) / (xMax - xMin));
        }

        public int JJ(double y)
        {
            return J2 + (int)((y - yMin) * (J1 - J2) / (yMax - yMin));
        }
        public Converter(int x, int y)
        {
            I2 = x;
            J2 = y;
        }
        public void Change(int fl)
        {
            double dx = xMax - xMin;
            double dy = yMax - yMin;
            switch (fl)
            {
                case 0:
                    xMin += dx * 0.1; xMax += dx * 0.1;
                    break;
                case 1:
                    xMin -= dx * 0.1; xMax -= dx * 0.1;
                    break;
                case 2:
                    yMin += dy * 0.1; yMax += dy * 0.1;
                    break;
                case 3:
                    yMin -= dy * 0.1; yMax -= dy * 0.1;
                    break;
            }
        }
        public void Scale(double m)
        {
            yMax *= m;
            yMin *= m;
            xMax *= m;
            xMin *= m;
        }
    }
}
