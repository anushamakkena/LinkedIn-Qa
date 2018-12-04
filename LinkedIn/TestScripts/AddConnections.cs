using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LinkedIn.PageObjects;
using LinkedIn.Utils;
using System.Collections.Generic;
using System.Threading;

namespace LinkedIn.TestScripts
{
    class AddConnections
    {
        static void Main(String[] args) { }
        
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void AddConnection()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            // Globale Variables Declaraton
            String URL = "https://www.linkedin.com";
            var emailID = "";
            var password = "";
            var searchText = "";
            var invitationMessage = "Hi! How are you doing? Let's connect here.";

            // Page Variables Declaration
            var loginPage = new LoginPage(driver);
            var logoutPage = new LogoutPage(driver);
            var homePage = new HomePage(driver);
            var utilsPage = new UtilityFunctionsPage(driver);

            // Navigate to LinkedIn SignIn page
            loginPage.goToSignInPage(URL);

            // Login User
            loginPage.logUser(emailID, password);
            
            // Search for People
            homePage.searchText(searchText);

            Thread.Sleep(5000);

            // get Search Results
            IList<IWebElement> peopleNamesList = driver.FindElements(By.XPath("//span[@class='name actor-name']"));
            String[] peopleNamesTextList = new String[peopleNamesList.Count];
            int i = 0;
            foreach (IWebElement element in peopleNamesList)
            {
                peopleNamesTextList[i++] = element.Text;
            }

            // verify search results contains searched Text
            foreach (string peopleName in peopleNamesTextList)
            {
                string actualText = peopleName.ToLower();
                Assert.IsTrue(actualText.Contains(searchText.ToLower()));
            }
           
            // connect all available People
            for(int j=0; j< peopleNamesList.Count; j++) 
            {
                string connectButtonXpath = $"(//button[contains(@class, 'search-result__actions--primary button')])[{j}]";

                if (utilsPage.IsElementPresent(By.XPath(connectButtonXpath)))                   
                {                   
                    // click on connect button
                    driver.FindElement(By.XPath(connectButtonXpath)).Click();
                    Thread.Sleep(1000);

                    // if Email is required to enter send email
                    if (utilsPage.IsElementPresent(By.XPath("//input[@id='email']")))
                    {
                        homePage.enterEmail(emailID);
                    }

                    // click on add a note button
                    homePage.clickAddANoteButton();
                    Thread.Sleep(1000);

                    // Enter Note to Invitation
                    homePage.sendInviteMessage(invitationMessage);
                    Thread.Sleep(1000);

                    // click on done button
                    homePage.clickDoneButton();
                    Thread.Sleep(2000);
                }
            }

            // Log Out User
            Thread.Sleep(5000);
            logoutPage.logoutUser();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
