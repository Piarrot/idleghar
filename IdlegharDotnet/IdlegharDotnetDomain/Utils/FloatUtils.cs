namespace IdlegharDotnetDomain.Utils
{
    public static class MathUtils
    {
        public static bool Equals(double x, double y)
        {
            return Equals(x, y, 0.001);
        }

        public static bool Equals(double x, double y, double closeness)
        {
            var result = Math.Abs(x - y);
            return result < closeness;
        }
    }
}