using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string value)
            => Regex.Replace(value ?? "", "[^0-9]", "");

        public static string Left(this string text, int length)
            => text[..(text.Length > length ? length - 1 : text.Length)];

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value) || !char.IsUpper(value[0]))
            {
                return value;
            }

            char[] chars = value.ToCharArray();
            FixCasing(chars);

            return new string(chars);
        }

        private static void FixCasing(Span<char> chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = i + 1 < chars.Length;

                // Stop when next char is already lowercase.
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    // If the next char is a space, lowercase current char before exiting.
                    if (chars[i + 1] == ' ')
                    {
                        chars[i] = char.ToLowerInvariant(chars[i]);
                    }

                    break;
                }

                chars[i] = char.ToLowerInvariant(chars[i]);
            }
        }

        public static bool CnpjValide(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) ||
                cnpj == "00000000000000")
            {
                return false;
            }

            var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int rest;
            string digit;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj[..12];
            sum = 0;
            for (var i = 0; i < 12; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            }

            rest = sum % 11;
            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();
            tempCnpj += digit;
            sum = 0;
            for (var i = 0; i < 13; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;
            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit += rest.ToString();
            return cnpj.EndsWith(digit);
        }

        public static bool CpfValide(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            var ls = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf, fmtCpf;
            try
            {
                fmtCpf = cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                tempCpf = fmtCpf;
                tempCpf = tempCpf[..9];

                int i;
                // Ex.: 1111
                for (i = 0; i < fmtCpf.Length - 1; i++)
                {
                    if (fmtCpf[i + 1] != fmtCpf[i])
                    {
                        break;
                    }
                }

                if (i == fmtCpf.Length - 1)
                {
                    return false;
                }

                tempCpf += CalculateDigit(tempCpf, ls);
                ls.Insert(0, 11);

                tempCpf += CalculateDigit(tempCpf, ls);

                return tempCpf.Equals(fmtCpf);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DateTime? ToDateOnly(this string date)
        {
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
                ? d
                : null;
        }

        public static DateTime? ToDateTime(this string date)
        {
            return DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
                ? d
                : null;
        }

        public static decimal? ToDecimal(this string amount)
        {
            return decimal.TryParse(amount, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out var d)
                ? d
                : null;
        }

        private static int CalculateDigit(string param, List<int> sequence)
        {
            int s = 0;
            for (int i = 0; i < param.Length; i++)
            {
                s += (int)char.GetNumericValue(param[i]) * sequence[i];
            }

            var aux = s % 11;
            return aux < 2 ? 0 : 11 - aux;
        }

        public static string GetJsonPropertyName<T>(this string propertyName)
        {
            string jsonPropertyName =
                typeof(T).GetProperties()
                .Where(p => p.Name == propertyName)
                .Select(p => p.GetCustomAttribute<JsonPropertyNameAttribute>())
                .Select(jp => jp.Name)
                .FirstOrDefault();

            if (null == jsonPropertyName)
            {
                throw new ArgumentException($"Type {nameof(T)} does not contain a property named {propertyName}");
            }

            return jsonPropertyName;
        }
    }
}