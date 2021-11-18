﻿using System.Reflection;

namespace gpconnect_user_portal.Helpers
{
    public class ApplicationHelper
    {
        public static class ApplicationVersion
        {
            public static string GetAssemblyVersion()
            {
                string buildTag = System.Environment.GetEnvironmentVariable("BUILD_TAG");

                if (string.IsNullOrWhiteSpace(buildTag))
                    return Assembly.GetEntryAssembly()?.GetName().FullName;

                return buildTag;
            }
        }
    }
}
