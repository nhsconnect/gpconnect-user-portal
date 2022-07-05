using GpConnect.NationalDataSharingPortal.Api.Helpers.Interfaces;

namespace GpConnect.NationalDataSharingPortal.Api.Helpers 
{
    public class PostCodeSanitiser: IPostCodeSanitiser
    {
        public string Sanitise(string input)
        {
            return input;
        }
    }
}