using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
     
    public class AuthBase : TestBase

    {

        [SetUp]

        public void Log()

        {

            AccountData user = new AccountData(Settings.Login, Settings.Password);

            app.Auth.Login(user);

        }

    }
}
