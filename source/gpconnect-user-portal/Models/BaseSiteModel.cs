using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.Pages
{
    public abstract class BaseSiteModel : BaseModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseSiteModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        [Display(Name = DisplayConstants.NAMEOFAUTHORISER)]
        [BindProperty(SupportsGet = true)]
        public string Authoriser { get; set; }

        [Display(Name = DisplayConstants.NAMEOFSUBMITTER)]
        [BindProperty(SupportsGet = true)]
        public string Submitter { get; set; }

        [Display(Name = DisplayConstants.CONTACTEMAILADDRESS)]
        [BindProperty(SupportsGet = true)]
        public string ContactEmailAddress { get; set; }

        [Display(Name = DisplayConstants.CONTACTTELEPHONE)]
        [BindProperty(SupportsGet = true)]
        public string ContactTelephone { get; set; }

        [Display(Name = DisplayConstants.ODSCODE)]
        [BindProperty(SupportsGet = true)]
        public string FormOdsCode { get; set; }

        [Display(Name = DisplayConstants.NOODSISSUED)]
        [BindProperty(SupportsGet = true)]
        public bool OdsIssued { get; set; }

        [Display(Name = DisplayConstants.SITENAME)]
        [BindProperty(SupportsGet = true)]
        public string SiteName { get; set; }

        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        [BindProperty(SupportsGet = true)]
        public string CcgOdsCode { get; set; }

        [Display(Name = DisplayConstants.CCGICBNAME)]
        [BindProperty(SupportsGet = true)]
        public string CcgName { get; set; }

        [Display(Name = DisplayConstants.ACTINGASSUPPLIER)]
        [BindProperty(SupportsGet = true)]
        public bool ActingAsSupplier { get; set; }

        [Display(Name = DisplayConstants.ACTINGASCONSUMER)]
        [BindProperty(SupportsGet = true)]
        public bool ActingAsConsumer { get; set; }

        [Display(Name = DisplayConstants.RECORDACCESSHTMLVIEW)]
        [BindProperty(SupportsGet = true)]
        public bool RecordAccessHtmlView { get; set; }

        [Display(Name = DisplayConstants.RECORDACCESSSTRUCTURED)]
        [BindProperty(SupportsGet = true)]
        public bool RecordAccessStructured { get; set; }

        [Display(Name = DisplayConstants.APPOINTMENT)]
        [BindProperty(SupportsGet = true)]
        public bool Appointment { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedUseCase { get; set; }

        [Display(Name = DisplayConstants.USECASEDESCRIPTION)]
        [BindProperty(SupportsGet = true)]
        public string UseCaseDescription { get; set; }

        [Display(Name = DisplayConstants.USECASE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> UseCases => GetUseCases();

        [BindProperty(SupportsGet = true)]
        public string SelectedSupplier { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedSupplierProduct { get; set; }

        [Display(Name = DisplayConstants.SELECTEDSUPPLIERPRODUCTUSECASE)]
        [BindProperty(SupportsGet = true)]
        public string SelectedSupplierProductUseCase { get; set; }

        [Display(Name = DisplayConstants.SUPPLIER)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> Suppliers => GetSuppliers();

        [Display(Name = DisplayConstants.SUPPLIERPRODUCT)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> SupplierProducts => GetSupplierProducts();

        private IEnumerable<SelectListItem> GetSuppliers()
        {
            var options = new List<SelectListItem>();
            for (int i = 0; i < 20; i++)
            {
                var option = $"SUPPLIER {i + 1}";
                options.Add(new SelectListItem() { Text = option, Value = option });
            }
            return options;
        }

        private IEnumerable<SelectListItem> GetSupplierProducts()
        {
            var options = new List<SelectListItem>();
            for (int i = 0; i < 20; i++)
            {
                var option = $"SUPPLIER PRODUCT {i + 1}";
                options.Add(new SelectListItem() { Text = option, Value = option });
            }
            return options;
        }

        private IEnumerable<SelectListItem> GetUseCases()
        {
            var options = new string[] { "NHS 111", "Hospital Access", "Social Care Access", "Emergency GP" };
            return options.Select(option => new SelectListItem() { Text = option, Value = option });
        }
    }
}