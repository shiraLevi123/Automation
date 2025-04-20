using MyAutomationProject.Tests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutomationProject.Pages
{
    public class RegistrationPage
    {
        private readonly IWebDriver driver; 
        FunctionLibrary functionLibrary = new FunctionLibrary();
        public RegistrationPage(IWebDriver driver)
        {
            this.driver=driver;
        }
        public void Register(string userName, string password)
        {
            driver.FindElement(By.Id("sign-username")).SendKeys(userName);
            driver.FindElement(By.Id("sign-password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[text() = 'Sign up']")).Click();
        }

        public string VerifyMessageAfterSignup(IWebDriver driver)
        {
            string alertText = null;

            try
            {
                if (functionLibrary.IsAlertShown(driver))
                    alertText = functionLibrary.CheckAlertMessage(driver);

                switch (alertText) 
                {
                    case "Sign up successful.":
                        functionLibrary.ClickAlert(driver, "אישור");
                        return alertText;

                    case "This user already exist.": 
                        functionLibrary.ClickAlert(driver, "אישור");
                        driver.FindElement(By.Id("sign-username")).Clear();
                        driver.FindElement(By.Id("sign-password")).Clear();
                        return alertText;

                    case "Please fill out Username and Password.":
                        functionLibrary.ClickAlert(driver, "אישור");
                        return alertText;

                    default:
                        Console.WriteLine("Not found an Alert Message!");
                        return alertText;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }

}
