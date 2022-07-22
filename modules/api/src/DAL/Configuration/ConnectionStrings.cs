namespace GpConnect.NationalDataSharingPortal.Api.Dal.Configuration
{
    public class ConnectionStrings
    {
        public ConnectionStrings()
        {
            DefaultConnection = "GpConnectEndUserPortal";
        }

        public string DefaultConnection { get; set; }
    }
}
