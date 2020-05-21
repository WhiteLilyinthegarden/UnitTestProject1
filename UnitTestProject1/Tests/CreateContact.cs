using System;
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
    public class CreateContact : TestBase
    {


        [Test]
        public void CreateContactTest()
        {
           
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Contact.OpenContactPage();
            ContactData contact = new ContactData("FM", "MN", "LN");
            app.Contact.AddNewContact(contact);
            app.Contact.OpenContactPage();
            app.Contact.LastContactEditButtonClick();

            ContactData newcontact = app.Contact.GetCreatedContactData();
            Assert.AreEqual(contact.Firstname, newcontact.Firstname);
            Assert.AreEqual(contact.Middlename, newcontact.Middlename);
            Assert.AreEqual(contact.Lastname, newcontact.Lastname);

            app.Auth.LogOut();
        }





    }
}

