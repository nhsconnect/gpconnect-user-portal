using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public partial class ReviewModel : BaseSiteModel
    {
        [Display(Name = DisplayConstants.AGREETODSAGREEMENT)]
        [BindProperty(SupportsGet = true)]
        public bool AgreeToDsAgreement { get; set; }

        [Display(Name = DisplayConstants.AGREETOUPDATEDPIA)]
        [BindProperty(SupportsGet = true)]
        public bool AgreeToUpdateDPIA { get; set; }

        [Display(Name = DisplayConstants.AGREEFORDIRECTCAREONLY)]
        [BindProperty(SupportsGet = true)]
        public bool AgreeForDirectCareOnly { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SiteInstanceId { get; set; }

        [Display(Name = DisplayConstants.USECASEDESCRIPTION)]
        [BindProperty(SupportsGet = true)]
        public string UseCaseDescription { get; set; }

        [Display(Name = DisplayConstants.USECASE)]
        [BindProperty(SupportsGet = true)]
        public string UseCase { get; set; }
    }
}