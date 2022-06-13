using TechTalk.SpecFlow;

using GpConnect.DataSharing.Admin.Specs.Drivers;
using GpConnect.DataSharing.Admin.Specs.PageObjects;

namespace GpConnect.DataSharing.Admin.Specs.Steps
{

    [Binding]
    public sealed class UsersStepsDefinitions
    {

        private readonly UsersPageObject _usersPageObject;

        public UsersStepsDefinitions(BrowserDriver browserDriver)
        {
            _usersPageObject = new UsersPageObject(browserDriver.Current);
        }


        [When("I navigate to the Users page")]
        public void WhenINavigateToUsersPage()
        {
            _usersPageObject.Open();
        }

    }



}
