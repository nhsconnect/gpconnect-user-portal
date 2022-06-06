namespace GpConnect.NationalDataSharingPortal.Api.Dal
{
    public class ConnectionStrings
    {
        public ConnectionStrings()
        {
            DefaultConnection = "GpConnectEndUserPortal";
        }

        public string DefaultConnection { get; set; }
    }


    //public class ConnectionStrings
    //{
    //    public string DefaultConnection { get; } = "GpConnectEndUserPortal";
    //}
}
