using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace LinkedIn.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // PAGE VARIABLES DECLARATION

        [FindsBy(How = How.ClassName, Using = "login-email")]
        private IWebElement emailInput;

        [FindsBy(How = How.ClassName, Using = "login-password")]
        private IWebElement passwordInput;

        [FindsBy(How = How.Id, Using = "login-submit")]
        private IWebElement signInButton;


        // FUNCTIONS

        // To Navigate LinkedIn Page
        public void goToSignInPage(String URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        // To Enter Email ID
        public void sendEmail(String emailID)
        {
            emailInput.SendKeys(emailID);
        }
        
        // To Enter Password
        public void sendPassword(String pwd)
        {
            passwordInput.SendKeys(pwd);
        }

        // To click on Sign In button
        public void clickSignIn()
        {
            signInButton.Click();
        }

        // To Log User
        public void logUser(String email, String pwd)
        {
            sendEmail(email);
            sendPassword(pwd);
            clickSignIn();
        }
    }
}
