namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{
    public class BasePageObject
    {

        private string baseUrl;
        protected TimeSpan DefaultWait = TimeSpan.FromSeconds(1);

        public BasePageObject()
        {
            baseUrl = Environment.GetEnvironmentVariable("BASE_ADMIN_URL") ?? "https://localhost:5001";
        }

        protected string URL(string path)
        {
            return baseUrl + path;
        }
    }
}
