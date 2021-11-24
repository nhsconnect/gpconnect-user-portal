using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Services.Interfaces;
using System.Linq;

namespace gpconnect_user_portal.Services
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService()
        {
        }

        public ApplicationDetail GetApplicationDetails()
        {
            var customAttributes = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(false);

            return new ApplicationDetail
            {
                ApplicationName = ((System.Reflection.AssemblyDescriptionAttribute)customAttributes.FirstOrDefault(x => x.GetType() == typeof(System.Reflection.AssemblyDescriptionAttribute))).Description,
                ApplicationEmailAddress = "gpconnect.enduserportal@nhs.net",
                AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
            };
        }
    }
}
