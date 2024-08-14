using System;
using System.ComponentModel;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttributeFromEnumType<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            Type type = value.GetType();
            MemberInfo[] members = type.GetMember(value.ToString());
            TAttribute attribute = members[0].GetCustomAttribute<TAttribute>(false);

            return attribute;
        }

        public static string GetDescription(this Enum enumValue)
            => enumValue.GetDescription(enumValue.ToString());

        public static string GetDescription(this Enum enumValue, string defaultValue)
            => enumValue.GetAttributeFromEnumType<DescriptionAttribute>()?.Description ?? defaultValue;

        public static object GetAmbientValue(this Enum enumValue)
            => enumValue.GetAmbientValue(default);

        public static object GetAmbientValue(this Enum enumValue, object defaultValue)
            => enumValue.GetAttributeFromEnumType<AmbientValueAttribute>()?.Value ?? defaultValue;

        public static string GetAmbientValueAsString(this Enum enumValue)
            => enumValue.GetAmbientValue()?.ToString();

        public static TType GetAmbientValueAs<TType>(this Enum enumValue)
        {
            object value = enumValue.GetAmbientValue();

            return value is not TType
                ? throw new InvalidCastException($"The value must be an '{typeof(TType).Name}' type")
                : value.ConvertTo<TType>();
        }

        public static T GetEnumFromDescription<T>(this string description) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }

        public static T GetEnumFromAmbientValue<T>(this object value) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(AmbientValueAttribute)) is AmbientValueAttribute attribute)
                {
                    if (attribute.Value.Equals(value))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
        }
    }
}