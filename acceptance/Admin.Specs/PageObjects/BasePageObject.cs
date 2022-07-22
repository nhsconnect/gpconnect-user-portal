namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{
    public class BasePageObject
    {

        private readonly string baseUrl;
        protected readonly TimeSpan DefaultWait;

        public BasePageObject()
        {
            DefaultWait = TimeSpan.FromSeconds(double.Parse(Environment.GetEnvironmentVariable("DEFAULT_TIMEOUT") ?? "1"));
            baseUrl = Environment.GetEnvironmentVariable("BASE_ADMIN_URL") ?? "https://localhost:5001";
        }

        protected string URL(string path)
        {
            return baseUrl + path;
        }
    }
}
