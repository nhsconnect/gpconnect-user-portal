namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class BasePageObject
    {

        private string baseUrl;
        protected TimeSpan DefaultWait = TimeSpan.FromSeconds(1);

        public BasePageObject()
        {
            baseUrl = Environment.GetEnvironmentVariable("BASE_USER_URL") ?? "https://localhost:5003";
        }

        protected string URL(string path)
        {
            return baseUrl + path;
        }
    }
}
