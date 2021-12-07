using gpconnect_user_portal.Helpers.Constants;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.DTO.Response
{
    public class SearchResultEntry
    {
        [Display(Name = DisplayConstants.SITENAME)]
        public string SiteName { get; set; }
        [Display(Name = DisplayConstants.SITEODSCODE)]
        public string SiteODSCode { get; set; }
        [Display(Name = DisplayConstants.CCGICBNAME)]
        public string CCGName { get; set; }
        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        public string CCGODSCode { get; set; }
        [Display(Name = DisplayConstants.RECORDACCESSHTMLVIEW)]
        public bool HasHtmlView { get; set; }
        [Display(Name = DisplayConstants.RECORDACCESSSTRUCTURED)]
        public bool HasStructured { get; set; }
        [Display(Name = DisplayConstants.APPOINTMENT)]
        public bool HasAppointment { get; set; }
        [Display(Name = DisplayConstants.USECASE)]
        public string UseCase { get; set; }
        public bool DisplayUseCase => !string.IsNullOrEmpty(UseCase);        
    }
}
