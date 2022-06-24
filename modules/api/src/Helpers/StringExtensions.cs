using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpConnect.NationalDataSharingPortal.Api.Helpers
{
    public static class StringExtensions
    {
        public static string? Flatten(IEnumerable elems, string separator)
        {
            elems = elems ?? throw new ArgumentNullException(nameof(elems));
            separator = separator ?? throw new ArgumentNullException(nameof(separator));

            var sb = new StringBuilder();
            foreach (object elem in elems)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(elem);
            }
            return sb.ToString();
        }

        public static string FirstCharToUpper(this string input, bool restToLower = false) =>
            input?.Trim() switch
            {
                null or "" => throw new ArgumentNullException(nameof(input)),
                _ => input.First().ToString().ToUpper() + (restToLower ? input.Substring(1).ToLower() : input.Substring(1))
            };

        public static string Coalesce(params string[] strings)
        {
            return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s));
        }

        public static string SearchAndReplace(this string input, Dictionary<string, string> replacementValues) =>
            input?.Trim() switch
            {
                null or "" => throw new ArgumentNullException(nameof(input)),
                _ => replacementValues == null ? throw new ArgumentNullException(nameof(replacementValues)) : replacementValues.Aggregate(input, (current, value) => current.Replace(value.Key, value.Value))
            };

        public static string FlattenStrings(params string[] strings)
        {
            strings = strings ?? throw new ArgumentNullException(nameof(strings));
            return string.Join(", ", strings.Where(s => !string.IsNullOrEmpty(s)));
        }

        public static string Pluraliser(this string input, int countValue, string startTag = "", string endTag = "") =>
            startTag + input?.Trim() switch
            {
                null or "" => throw new ArgumentNullException(nameof(input)),
                _ => countValue == 1 ? string.Format(input, countValue, string.Empty) : countValue == 0 ? string.Empty : string.Format(input, countValue, "s")
            } + endTag;
    }
}
