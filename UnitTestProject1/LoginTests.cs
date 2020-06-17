using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidData()
        {
            if (app.Auth.IsLoggedIn()) app.Auth.LogOut();
            
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData(Settings.Login, Settings.Password);
            app.Auth.Login(user);
            Assert.IsTrue(app.Auth.IsLoggedIn(Settings.Login));
            // залогиненеа с корр юзеро вызвать из лог ин 
        }
        [Test]
        public void LoginWithInvalidData()
        {
            if (app.Auth.IsLoggedIn()) app.Auth.LogOut();
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "nosecret");
            app.Auth.Login(user);
            Thread.Sleep(1000);
            Assert.IsFalse(app.Auth.IsLoggedIn());
            // залогиненеа с некорр юзеро вызвать из лог ин ассерт выходит фолс
        }
    }
}
