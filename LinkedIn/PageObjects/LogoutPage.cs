using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedIn.PageObjects
{
    class LogoutPage
    {
        private IWebDriver driver;

        // Constructor
        public LogoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // PAGE VARIABLES DECLARATION

        [FindsBy(How = How.XPath, Using = "//div[@id='nav-settings__dropdown']/button/div/span[2]")]
        private IWebElement settingsDropdown;

        [FindsBy(How = How.XPath, Using = "//a[@data-control-name='nav.settings_signout']")]
        private IWebElement signOutLink;


        // FUNCTIONS

        // To click on User Settings Dropdown
        public void clickSettingsDropdown()
        {
            settingsDropdown.Click();
        }
        
        // To click on Sign Out Link
        public void clickSignOut()
        {
            signOutLink.Click();
        }

        // to log out user
        public void logoutUser()
        {
            clickSettingsDropdown();
            clickSignOut();
        }
    }
}
