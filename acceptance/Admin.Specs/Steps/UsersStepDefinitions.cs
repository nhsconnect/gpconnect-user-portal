using TechTalk.SpecFlow;

using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

namespace Admin.Specs.Steps
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
