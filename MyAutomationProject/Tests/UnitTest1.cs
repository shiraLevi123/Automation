using MyAutomationProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyAutomationProject.Tests
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        private string url = "https://www.demoblaze.com/";

        string existUserName = "shira1234";
        string userName = "shira1234" + DateTime.Now;
        string password = "12345678";
        string productTitle = "Sony vaio i5";
        string productPrice = "790";
        FunctionLibrary functionLibrary = new FunctionLibrary();
        string alertText = null;


        [TestInitialize]

        public void SetUp() {
            driver = new ChromeDriver(@"M:\פולסטאק שנה ב תשפ''ה\Automation");
            //driver = new ChromeDriver(@"C:\Automation");

            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
        }

        [TestMethod]
        [DataRow(DisplayName = "Sanity")] 
        public void TestScenario()
        {
            RegistrationTest();
            LoginTestValidation();
            LoginTest();
            AddToCart();
            PlaceOrder();

        }
        public void RegistrationTest()
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToRegistrationPage();

            RegistrationPage registrationPage = new RegistrationPage(driver);

            registrationPage.Register(existUserName, password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreNotEqual("Sign up successful.", alertText);

            registrationPage.Register("", password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreNotEqual("Sign up successful.", alertText);

            registrationPage.Register(userName, password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreEqual("Sign up successful.", alertText, "Registration failed.");
            Console.WriteLine(userName);

        }

        public void LoginTestValidation() 
        {
            var homePage = new HomePage(driver);
            homePage.GoToLoginPage(); 
            var loginPage = new LoginPage(driver);
            Assert.IsTrue(loginPage.LoginValidationTest(driver, userName));
        }

        public void LoginTest() 
        {
            HomePage homePage = new HomePage(driver);

            var loginPage = new LoginPage(driver);
            loginPage.Login(existUserName, password);

            Assert.IsTrue(homePage.IsLoggedIn(existUserName), "Login failed."); 
        }
        public void AddToCart()
        {
            string productAddedExpectedMsg = "Product added.";
            string actualAlertMsg = null;

            HomePage homePage = new HomePage(driver);

            homePage.SelectCategory("Laptops"); 
            homePage.GoToProductPage(productTitle); 
            homePage.AddProductToCart(); 

            if (functionLibrary.IsAlertShown(driver))
                actualAlertMsg = functionLibrary.CheckAlertMessage(driver); 

            if (actualAlertMsg.Equals(productAddedExpectedMsg)) 
            {
                functionLibrary.ClickAlert(driver, "אישור");
                Console.WriteLine("Product added"); 
            }


            homePage.GoToCartPage(); //מעבר לדף עגלה
            CartPage cartPage = new CartPage(driver);

            Assert.IsTrue(cartPage.IsProductInCart(productTitle), "Product not found in cart."); 
        }
        public void PlaceOrder()
        {
            CartPage cartPage = new CartPage(driver);
            PaymentPage paymentPage = new PaymentPage(driver);
            HomePage homePage = new HomePage(driver);
            if(cartPage.VerifyProductHasBeenAddedToCart(driver, productTitle, productPrice))
            {
                cartPage.ClickOnPlaceOrder(driver);
                paymentPage.FillForm(driver);
                paymentPage.ClickOnPurchaseBtn(driver);
                paymentPage.ValidatePurchase(driver);

                Assert.IsTrue(paymentPage.PrintId(driver));
                paymentPage.ClickOkButton(driver); 
                homePage.ClickOnLogOut();
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            driver.Quit();
        }

    }
}