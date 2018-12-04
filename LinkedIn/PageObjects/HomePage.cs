using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedIn.PageObjects
{
    class HomePage
    {
        private IWebDriver driver;

        // Constructor
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // PAGE VARIABLES DECLARATION

        [FindsBy(How = How.XPath, Using = "//*[@class='ember-view']/input")]
        public IWebElement searchInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='next-text']")]
        public IWebElement nextButton;

        [FindsBy(How = How.XPath, Using = "//input[@id='email']")]
        public IWebElement emailInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='send-invite__actions']/button[contains(@class, 'button-secondary')]")]
        private IWebElement addANoteButton;

        [FindsBy(How = How.XPath, Using = "//textarea[@id='custom-message']")]
        private IWebElement sendInviteMessageTextarea;

        [FindsBy(How = How.XPath, Using = "//div[@class='send-invite__actions']/button[2]")]
        private IWebElement doneButton;



        // FUNCTIONS
        
        public void searchText(String text)
        {
            searchInput.Click();
            searchInput.SendKeys(text);
            searchInput.SendKeys(Keys.Enter);
        }

        public void enterEmail(String text)
        {
            emailInput.Click();
            emailInput.SendKeys(text);
        }

        public void clickAddANoteButton()
        {
            addANoteButton.Click();
        }
        
        public void sendInviteMessage(string invitationMessage)
        {
            sendInviteMessageTextarea.SendKeys(invitationMessage);
        }
        
        public void clickDoneButton()
        {
            doneButton.Click();
        }    
    }
}
