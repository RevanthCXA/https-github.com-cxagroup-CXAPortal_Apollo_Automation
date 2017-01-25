using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using CXAPortal.TestDataAccess;
using CXAPortal.Utility_Scripts;

namespace CXAPortal.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        [FindsBy(How = How.LinkText, Using = "Log In")][CacheLookup]
        public IWebElement Login_Link { get; set; }

        [FindsBy(How = How.Id, Using = "CompanyID")]
        [CacheLookup]
        public IWebElement Company_ID { get; set; }

        [FindsBy(How = How.Id, Using = "LoginID")]
        [CacheLookup]
        public IWebElement Login_ID { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        [CacheLookup]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "SignIn")]
        [CacheLookup]
        public IWebElement SignIn_button { get; set; }
        public object UserName { get; private set; }

        //[FindsBy(How = How.XPath, Using = "//*[@id='menubar']/table/tbody/tr/td[2]/a[2]")]
        [FindsBy(How = How.LinkText, Using = "LOGOUT")]
        [CacheLookup]
        public IWebElement Logout { get; set; }

        /// <summary>
        /// Initilizing the variables
        /// </summary>
        /// <param name="driver"></param>
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public void LoginToApplication(string testName)
        {

            Console.Write("Login function");
        }
    }
}
