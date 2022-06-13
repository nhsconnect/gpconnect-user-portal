using TechTalk.SpecFlow;

using Npgsql;

using GpConnect.DataSharing.Admin.Specs.Drivers;

namespace GpConnect.DataSharing.Admin.Specs.Hooks
{

    [Binding]
    public class AdminHooks
    {
        [AfterScenario]
        public static void DeleteUsers(DataDriver dataDriver)
        {
            dataDriver.TruncateUsers();
        }
    }
}
