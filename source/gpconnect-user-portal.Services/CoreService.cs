using gpconnect_user_portal.DTO.Response.Core;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;

namespace gpconnect_user_portal.Services
{
    public class CoreService : ICoreService
    {
        private readonly IOptionsMonitor<DTO.Response.Configuration.Email> _emailOptionsDelegate;

        public CoreService(IOptionsMonitor<DTO.Response.Configuration.Email> emailOptionsDelegate)
        {
            _emailOptionsDelegate = emailOptionsDelegate;
        }

        public ApplicationDetail GetApplicationDetails()
        {
            var customAttributes = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(false);

            return new ApplicationDetail
            {
                ApplicationName = ((System.Reflection.AssemblyDescriptionAttribute)customAttributes.FirstOrDefault(x => x.GetType() == typeof(System.Reflection.AssemblyDescriptionAttribute))).Description,
                ApplicationEmailAddress = _emailOptionsDelegate.CurrentValue.SenderAddress,
                AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
            };
        }
    }
}
