namespace EXM.Common.Tools.Extensions
{
    public static class DoubleExtensions
    {
        public static double Round(this double value, int roundTo)
        {
            return (int)(Math.Round(value / roundTo) * roundTo);
        }
    }
}
