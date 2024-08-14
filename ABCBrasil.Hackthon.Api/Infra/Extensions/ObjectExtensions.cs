using System;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class ObjectExtensions
    {
        public static TType ConvertTo<TType>(this object value)
            => (TType)Convert.ChangeType(value, typeof(TType));

        public static TType ConvertTo<TType>(this object value, TType defaultValue)
        {
            try
            {
                return value.ConvertTo<TType>();
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}