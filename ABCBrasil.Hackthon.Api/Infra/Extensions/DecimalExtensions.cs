using System.Globalization;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToAmountUs(this decimal amount)
        {
            return amount.ToString("0.00", new CultureInfo("en-US"));
        }

        public static string ToAmountUs(this decimal amount, string format)
        {
            return amount.ToString(format, new CultureInfo("en-US"));
        }
    }
}