using System.Collections.Generic;

namespace gpconnect_user_portal.Helpers.Constants
{
    public class SearchConstants
    {
        public static Dictionary<int, string> SortOptions = new Dictionary<int, string>()
        {
            { 0, string.Empty },
            { 1, DisplayConstants.NOHTMLVIEW },
            { 2, DisplayConstants.HASHTMLVIEW },
            { 3, DisplayConstants.NOSTRUCTURED },
            { 4, DisplayConstants.HASSTRUCTURED },
            { 5, DisplayConstants.NOAPPOINTMENT },
            { 6, DisplayConstants.HASAPPOINTMENT }
        };

        public const string HtmlQueryFilterInteraction = "gpc.getcarerecord";
        public const string StructuredQueryFilterInteraction = "structured:fhir:rest:read:metadata-1";
        public const string AppointmentQueryFilterInteraction = "appointments-1";

        public const string SiteNameAttributeName = "SiteName";
        public const string CCGOdsCodeAttributeName = "SelectedCCGOdsCode";
        public const string CCGNameAttributeName = "SelectedCCGName";

    }
}
