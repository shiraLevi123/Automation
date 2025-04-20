using MyAutomationProject.Tests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutomationProject.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public IWebElement loginusername;
        public IWebElement loginpassword;
        public string alertText;
        public IWebElement loginBtn; 

        FunctionLibrary functionLibrary = new FunctionLibrary();

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            loginusername = driver.FindElement(By.Id("loginusername"));
            loginpassword = driver.FindElement(By.Id("loginpassword"));
            loginBtn = driver.FindElement(By.CssSelector("button[onclick='logIn()']"));
        }
        public void Login(string username, string password)
        {
            loginusername.Clear();
            loginpassword.Clear();
            loginusername.SendKeys(username);
            loginpassword.SendKeys(password);
            loginBtn.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        public void Login1(string username, string password)
        {

            loginusername.SendKeys(username);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            loginpassword.SendKeys(password);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            loginBtn.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public bool LoginValidationTest(IWebDriver driver, string userName)
        {
            try
            {
                string wrongPassword = "000";
                string expectedResultWrongPassword = "Wrong password.";

                loginusername.Clear();   
                loginpassword.Clear();
                loginusername.SendKeys(userName); 
                loginpassword.SendKeys(wrongPassword); 
                loginBtn.Click();

                if (functionLibrary.IsAlertShown(driver)) 
                {
                    alertText = functionLibrary.CheckAlertMessage(driver);

                    if (alertText.Equals(expectedResultWrongPassword)) 
                        if (!functionLibrary.ClickAlert(driver, "אישור")) 
                            Assert.Fail(); 
                        else
                            return true;
                }

                return false;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
