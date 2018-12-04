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
    class AddConncetionsScenario2
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
        public void AddConnections()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            // Globale Variables Declaraton
            String URL = "https://www.linkedin.com";
            var emailID = "";
            var password = "";
            var searchText = "";
            var city = "";
            var invitationMessage = "Hi! How are you doing? Let's connect here.";
            int pageCount = 3;

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(2000);

            // Scroll down the page
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
            Thread.Sleep(2000);

            // Scroll to Top
            js.ExecuteScript("arguments[0].scrollIntoView(false);", homePage.searchInput);
            Thread.Sleep(2000);

            // iterate loop for number of pages to add conncetions
            for (int i=0; i< pageCount; i++)
            {
                // get Search Results
                IList<IWebElement> peopleNamesList = driver.FindElements(By.XPath("//span[@class='name actor-name']"));

                // iterating loop to connect all available People
                for (int j = 0; j < peopleNamesList.Count; j++)
                {
                    Thread.Sleep(1000);
                    string connectButtonXpath = $"(//button[contains(@class, 'search-result__actions--primary button')])[{j}]";
                    
                    // if the search text is having keyword city
                    if(city != "")
                    {
                        string cityNameXpath = $"(//p[contains(@class, 'subline-level-2')]/span)[{j+1}]";
                        string cityName_UI = driver.FindElement(By.XPath(cityNameXpath)).Text;
                        Thread.Sleep(1000);

                        // Add People who are in the mentioned city
                        if (cityName_UI.ToLower().Contains(city.ToLower()))
                        {
                            // Add People
                            if (utilsPage.IsElementPresent(By.XPath(connectButtonXpath)))
                            {
                                // Scroll the element connect button into view for interactions
                                js.ExecuteScript("arguments[0].scrollIntoView(false);", driver.FindElement(By.XPath(connectButtonXpath)));
                                Thread.Sleep(1000);

                                // click on connect button
                                js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(connectButtonXpath)));
                                Thread.Sleep(1000);

                                // if Email is required to enter then send email
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
                    }
                    else
                    {
                        // Add people
                        if (utilsPage.IsElementPresent(By.XPath(connectButtonXpath)))
                        {
                            // Scroll the element connect button into view for interactions
                            js.ExecuteScript("arguments[0].scrollIntoView(false);", driver.FindElement(By.XPath(connectButtonXpath)));
                            Thread.Sleep(1000);

                            // click on connect button
                            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(connectButtonXpath)));
                            Thread.Sleep(1000);

                            // if Email is required to enter then send email
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
                }
                   
                // Navigation to next page
                if(i == pageCount-1) { }
                else
                {
                    // Scroll down the page
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    // verify next button is available
                    if (utilsPage.IsElementPresent(By.XPath("//div[@class='next-text']")))
                    {
                        // click on next button
                        driver.FindElement(By.XPath("//div[@class='next-text']")).Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    }
                    else
                    {
                        i = pageCount;
                    }
                }
            }

            // Log Out User
            Thread.Sleep(3000);
            logoutPage.logoutUser();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
