using Microsoft.AspNetCore.Html;

namespace gpconnect_user_portal.Helpers
{
    public static class BooleanExtensions
    {
        public static HtmlString BooleanToHtml(this bool input) =>
            input switch
            {
                _ => input ? new HtmlString("&#x2713;") : new HtmlString("&#x2717;")
            };

        public static HtmlString StringToBooleanHtml(this string input) =>
            input switch
            {
                _ => bool.TryParse(input, out _) ? new HtmlString("&#x2717;") : new HtmlString("&#x2713;")
            };

        public static string BooleanToYesNo(this bool input) =>
            input switch
            {
                _ => input ? "Yes" : "No"
            };
    }
}
