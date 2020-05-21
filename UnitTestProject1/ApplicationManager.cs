using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
   public class ApplicationManager 
    {
        protected IWebDriver driver;
        private string baseURL;
        private NavigationHelper navigation;
        private LoginHelper auth;
        private GroupHelper group;
        private ContactHelper contact;
        private HelperBase helper;

        public ApplicationManager()
        {
            driver = new FirefoxDriver(@"C:\distr");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            baseURL = "http://localhost/addressbook/";
           // verificationErrors = new StringBuilder();
            group = new GroupHelper(this);
            contact = new ContactHelper(this);
            auth = new LoginHelper(this);
            navigation = new NavigationHelper(this, baseURL);
            helper = new HelperBase(this);

        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        public void Stop()
        {
            driver.Quit();
        }
        public NavigationHelper Navigation
        {
            get
            {
                return navigation;
            }

        }
        public LoginHelper Auth
        {
            get
            {
                return auth;
            }
        }
        public GroupHelper Group
        {
            get
            {
                return group;
            }
        }
        public ContactHelper Contact
        {
            get
            {
                return contact;
            }
        }
        public HelperBase Helper
        {
            get
            {
                return helper;
            }
        }
    }
}
