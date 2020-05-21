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
    public class LoginHelper : HelperBase
    {
        private LoginPage loginPage;

        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }
        public void Login(AccountData user)
        {
            FillTheField(LoginPage.UserTextField, user.Username);
            FillTheField(LoginPage.PasswordTextField, user.Password);
            Click(LoginPage.SubmitLoginButton);

        }


        public void LogOut()
        {
            // driver.FindElement(By.LinkText("Logout")).Click();
            Click(LoginPage.SubmitLogOutButton);
        }
        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null)
                {
                    loginPage = new LoginPage();
                }
                return loginPage;
            }
        }
    }
}
