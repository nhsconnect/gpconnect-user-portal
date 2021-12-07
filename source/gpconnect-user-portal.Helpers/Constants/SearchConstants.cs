using System.Collections.Generic;

namespace gpconnect_user_portal.Helpers.Constants
{
    public class SearchConstants
    {
        public const string PROVIDERODSCODEVALIDERRORMESSAGE = "You must enter a valid provider ODS code";

        public static Dictionary<string, string> SortOptions = new Dictionary<string, string>()
        {
            { string.Empty, DisplayConstants.SORTBY },
            { DisplayConstants.NOHTMLVIEW, DisplayConstants.NOHTMLVIEW },
            { DisplayConstants.HASHTMLVIEW, DisplayConstants.HASHTMLVIEW },
            { DisplayConstants.NOSTRUCTURED, DisplayConstants.NOSTRUCTURED },
            { DisplayConstants.HASSTRUCTURED, DisplayConstants.HASSTRUCTURED },
            { DisplayConstants.NOAPPOINTMENT, DisplayConstants.NOAPPOINTMENT },
            { DisplayConstants.HASAPPOINTMENT, DisplayConstants.HASAPPOINTMENT }
        };
    }
}
