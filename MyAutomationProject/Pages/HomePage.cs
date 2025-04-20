using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutomationProject.Pages
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

            public void GoToRegistrationPage()
        {
            driver.FindElement(By.Id("signin2")).Click();
        }
        public void GoToLoginPage()
        {
            driver.FindElement(By.Id("login2")).Click();
        }
        public bool IsLoggedIn(string username)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nameofuser"))); 

            return driver.FindElement(By.Id("nameofuser")).Text.Contains(username);
        }
        public void SelectCategory(string category) 
        {
            driver.FindElement(By.XPath(string.Format("//a[text()='{0}']", category))).Click();

        }

        public void GoToProductPage(string productName)
        {
            driver.FindElement(By.LinkText(productName)).Click();
        }

        public void AddProductToCart() 
        {
            driver.FindElement(By.LinkText("Add to cart")).Click();
        }
        public void GoToCartPage() 
        {
            driver.FindElement(By.Id("cartur")).Click();
        }
        public void ClickOnLogOut()
        {
            driver.FindElement(By.Id("logout2")).Click();
        }
    }
}
