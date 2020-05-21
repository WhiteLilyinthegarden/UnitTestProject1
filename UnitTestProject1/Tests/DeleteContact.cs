using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
    {
    [TestFixture]
    public class DeleteContact : TestBase
    {


        [Test]
        public void TheDeleteContactTest()
        {
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Contact.OpenContactPage();
            if (app.Helper.FindMaxItem() == null)
            {
                ContactData contact = new ContactData("FM", "MN", "LN");
                app.Contact.AddNewContact(contact);
                app.Contact.OpenContactPage();

            }

            int id = app.Helper.FindAndClickMaxItems();
            app.Contact.DeleteLastContact();
            app.Contact.OpenContactPage();
            Assert.IsTrue(app.Helper.IsIdDeleted(id, "id"));

            app.Auth.LogOut();

        }

    }    
}

