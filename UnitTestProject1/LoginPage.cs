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
    public class LoginPage
    {
        public By UserTextField { get { return By.Name("user"); } }
        public By PasswordTextField { get { return By.Name("pass"); } }
        public By SubmitLoginButton { get { return By.XPath("//input[@value='Login']"); } }
        public By SubmitLogOutButton { get { return By.LinkText("Logout"); } }
    }
}

