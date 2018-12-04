using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LinkedIn.Utils
{
    class UtilityFunctionsPage
    {
        private IWebDriver driver;

        // Constructor
        public UtilityFunctionsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
