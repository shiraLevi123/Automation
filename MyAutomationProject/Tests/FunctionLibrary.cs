using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutomationProject.Tests
{
    public class FunctionLibrary
    {
        public bool IsAlertShown(IWebDriver driver)
        {
            try
            {
                //המתנה עד שהאלרט מופיע
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                driver.SwitchTo().Alert();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //חילוץ ההודעה מתוך ה - Alert
        public string CheckAlertMessage(IWebDriver driver)
        {
            string text;
            try
            {
                IAlert alert;
                Thread.Sleep(1000);

                alert = driver.SwitchTo().Alert();
                text = alert.Text;

                return text;

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public bool ClickAlert(IWebDriver driver, string type)
        {
            IAlert alert;
            alert = driver.SwitchTo().Alert();
            if (type == "אישור")
            {
                alert.Accept();
                return true;
            }

            else if (type == "Cancel")
            {
                alert.Dismiss();
                return true;
            }
            return false;
        }
    }


}
