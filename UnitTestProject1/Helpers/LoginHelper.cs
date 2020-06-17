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
            if (IsLoggedIn())
            {
                if (IsLoggedIn(user.Username))
                {
                    return;
                }
                LogOut();
            }

            //LoginCheck(user.Username);
            FillTheField(LoginPage.UserTextField, user.Username);
            FillTheField(LoginPage.PasswordTextField, user.Password);
            Click(LoginPage.SubmitLoginButton);

        }


        public void LogOut()
        {
            // driver.FindElement(By.LinkText("Logout")).Click();
            Click(LoginPage.SubmitLogOutButton);
            WaitUntilVisible(LoginPage.SubmitLoginButton);
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
        public bool IsLoggedIn()
        {
            //if (driver.FindElement(LoginPage.SubmitLogOutButton).Enabled)
            //{
            //    return true;
            //}
            //return false;
            return IsElementPresent(LoginPage.SubmitLogOutButton);
        }
        public bool IsLoggedIn(string username)
        {
            string loginInName = driver.FindElement(LoginPage.LoginNameText).Text;
            username = "("+ username +") Logout";
            if (loginInName.Equals(username))
            {
                return true;
            }
            return false;
        }
        /*public void LoginCheck(string user_temp)
        {

            if (IsLoggedIn())
            {
                if (IsLoggedIn(user_temp))
                {
                    return;
                }
                LogOut();


            }
        }*/
    }
}