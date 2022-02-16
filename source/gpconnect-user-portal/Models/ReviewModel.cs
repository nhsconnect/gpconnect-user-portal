using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public partial class ReviewModel : BaseSiteModel
    {
        [Display(Name = DisplayConstants.USECASEDESCRIPTION)]
        [BindProperty(SupportsGet = true)]
        public string UseCaseDescription { get; set; }

        [Display(Name = DisplayConstants.USECASE)]
        [BindProperty(SupportsGet = true)]
        public string UseCase { get; set; }        

        //public new bool IsHtmlEnabled => GetAttributeValue("RecordAccessHtmlView") != null;
        //public new bool IsAppointmentEnabled => GetAttributeValue("Appointment") != null;
        //public new bool IsStructuredEnabled => GetAttributeValue("RecordAccessStructured") != null;        
    }
}