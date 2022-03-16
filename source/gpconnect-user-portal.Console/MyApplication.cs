using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Console
{
    public class MyApplication
    {
        private readonly ILogger _logger;

        public MyApplication(ILogger<MyApplication> logger)
        {
            _logger = logger;
        }

        internal void Run()
        {
            _logger.LogInformation($"Gets here at {DateTime.UtcNow}");

            System.Console.WriteLine("PRESS <ENTER> TO EXIT");
            System.Console.ReadKey();
        }
    }
}
