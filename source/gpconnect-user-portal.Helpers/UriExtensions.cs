﻿using Microsoft.AspNetCore.Http;
using System;

namespace gpconnect_user_portal.Helpers
{
    public static class UriExtensions
    {
        public static Uri GetAbsoluteUri(this HttpContext httpContext)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = httpContext.Request.Scheme;
            uriBuilder.Host = httpContext.Request.Host.Host;
            uriBuilder.Path = httpContext.Request.Path.ToString();
            uriBuilder.Query = httpContext.Request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        public static string GetBaseSiteUrl(this HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/";
        }
    }
}
