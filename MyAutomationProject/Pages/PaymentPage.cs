using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutomationProject.Pages
{
    public class PaymentPage
    {
        IWebDriver driver;
        string name = "Shira Levi";
        string country = "Israel";
        string city = "Petah Tikwa";
        string card = "1234567890";
        string month = "08";
        string year = "2030";

        public PaymentPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void FillForm(IWebDriver driver)
        {
            driver.FindElement(By.Id("name")).SendKeys(name);
            driver.FindElement(By.Id("country")).SendKeys(country);
            driver.FindElement(By.Id("city")).SendKeys(city);
            driver.FindElement(By.Id("card")).SendKeys(card);
            driver.FindElement(By.Id("month")).SendKeys(month);
            driver.FindElement(By.Id("year")).SendKeys(year);
        }
        public void ClickOnPurchaseBtn(IWebDriver driver)
        {
            driver.FindElement( By.XPath("//button[text() = 'Purchase']")).Click();
        }
        public bool ValidatePurchase(IWebDriver driver)
        {
            try
            {
                if (CheckTitle(driver))
                    if (CheckData(driver, name, card))
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckData(IWebDriver driver,string name,string card) 

        {
            try
            {
                string text = driver.FindElement(By.XPath("//*[@class = 'lead text-muted ']")).Text;
                text = text.Replace("\n", "").Replace("\r", "");
               
                if (text.Contains(name) && text.Contains(card))
                    return true;

                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool CheckTitle(IWebDriver driver) 
        {
            try
            {
                string title = "Thank you for your purchase!";
                IWebElement thankYouElm = driver.FindElement(By.XPath("//*[@class = 'sweet-alert  showSweetAlert visible']"));
                var text = thankYouElm.FindElement(By.TagName("h2")).Text; //חילוץ טקסט מתוך ההודעה
                if (title.Equals(text))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool PrintId(IWebDriver driver)
        {
            try
            {
                string text = driver.FindElement(By.XPath("//*[@class = 'lead text-muted ']")).Text;
                int startPos = text.IndexOf("Id");
                int length = text.IndexOf("\r");
                string sub = text.Substring(startPos, length);
                return true;
            }
            catch { return false; }
        }
        public void ClickOkButton(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@class = 'confirm btn btn-lg btn-primary']")).Click();
        }

    }
}
