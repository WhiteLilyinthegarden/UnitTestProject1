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
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            OpenContactPage();
            ContactData contact = new ContactData("FM", "MN", "LN");
            AddNewContact(contact);
            OpenContactPage();
            LastContactEditButtonClick();

            ContactData newcontact = GetCreatedContactData();
            Assert.AreEqual(contact.Firstname, newcontact.Firstname);
            Assert.AreEqual(contact.Middlename, newcontact.Middlename);
            Assert.AreEqual(contact.Lastname, newcontact.Lastname);

            LogOut();
        }





    }
}

