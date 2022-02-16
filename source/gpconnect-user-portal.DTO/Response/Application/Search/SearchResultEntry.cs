using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Helpers.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Application.Search
{
    public class SearchResultEntry
    {
        public int SiteDefinitionId { get; set; }

        [Display(Name = DisplayConstants.SITEODSCODE)]
        public string SiteODSCode { get; set; }

        public Guid SiteUniqueIdentifier { get; set; }

        public int SiteDefinitionStatusId { get; set; }

        public string SiteInteractions { get; set; }
        public string SiteAttributes { get; set; }

        public Dictionary<string, string> SiteAttributesDictionary => ExtractSiteAttributesDictionary();

        private Dictionary<string, string> ExtractSiteAttributesDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(SiteAttributes);
        }

        public bool DisplayUseCase => SiteAttributesDictionary.Count(x => x.Key == "UseCaseDescription" && !string.IsNullOrEmpty(x.Value)) == 1;

        [Display(Name = DisplayConstants.HASHTMLVIEW)]
        public bool HasHtmlView => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.HtmlQueryFilterInteraction);
        public string HasHtmlViewAsText => HasHtmlView.BooleanToYesNo();

        [Display(Name = DisplayConstants.HASSTRUCTURED)] 
        public bool HasStructured => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.StructuredQueryFilterInteraction);
        public string HasStructuredAsText => HasStructured.BooleanToYesNo();

        [Display(Name = DisplayConstants.HASAPPOINTMENT)]
        public bool HasAppointment => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.AppointmentQueryFilterInteraction); 
        public string HasAppointmentAsText => HasAppointment.BooleanToYesNo();        
    }
}
