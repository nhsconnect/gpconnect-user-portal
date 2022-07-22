namespace GpConnect.NationalDataSharingPortal.Api.Dal.Authentication.Interface
{
    public interface IAuthTokenGenerator
    {
        string GenerateAuthToken(string host, int port, string user);
    }
   
}