using System.Globalization;

namespace ABCBrasil.Hackthon.Api.Infra.Providers.CultureProvider
{
    public class CultureGlobal
    {
        public static CultureInfo GetCultureInfo(Settings settings)
        {
            return new CultureInfo(settings.LanguageCode)
            {
                DateTimeFormat =
            {
                ShortDatePattern = settings.ShortDatePattern,
                DateSeparator = settings.DateSeparator
            }
            };
        }
    }
}