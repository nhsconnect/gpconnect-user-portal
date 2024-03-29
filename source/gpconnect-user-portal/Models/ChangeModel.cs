﻿using gpconnect_user_portal.DTO.Response.Application.Search;
using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.Pages
{
    public partial class ChangeModel : SearchBaseModel
    {
        public SearchResult SearchResults { get; set; }

        [Display(Name = DisplayConstants.PROVIDERODSCODE)]
        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessage = MessageConstants.ODSCODEVALIDVALUEERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string ProviderOdsCode { get; set; }

        [Display(Name = DisplayConstants.PROVIDERNAME)]
        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        public bool IsValidSearch => CheckForValidSearch();
        public bool HasMultipleSearchParameters => HasMultipleSearchValues();

        public string ReferrerUrl => GetReferrerQueryString();

        private string GetReferrerQueryString()
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if(ProviderOdsCode != null) queryString.Add("ProviderOdsCode", ProviderOdsCode);
            if(ProviderName != null) queryString.Add("ProviderName", ProviderName);
            if(SelectedCCGName != null) queryString.Add("SelectedCCGName", SelectedCCGName);
            if(SelectedCCGOdsCode != null) queryString.Add("SelectedCCGOdsCode", SelectedCCGOdsCode);
            return queryString.ToString();
        }

        private bool HasMultipleSearchValues()
        {
            var multipleSearchParametersEntered = new string[] { ProviderOdsCode, ProviderName, SelectedCCGName, SelectedCCGOdsCode };
            return multipleSearchParametersEntered.Count(s => !string.IsNullOrEmpty(s)) > 1;
        }

        private bool CheckForValidSearch()
        {
            return !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName) || !string.IsNullOrEmpty(SelectedCCGName) || !string.IsNullOrEmpty(SelectedCCGOdsCode);
        }

        public bool DisplaySearchInvalid { get; set; } = false;
    }
}