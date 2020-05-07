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
        public class EditContact :TestBase
       {
        

            [Test]
            public void EditcontactTest()
        {
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            OpenContactPage();
            ContactData contact = new ContactData("FM", "MN", "LN");
            ContactData editContact = new ContactData("EditFM", "EditMN", "EditLN");
            if (FindMaxItem() == null)
            {
                AddNewContact(contact);
                OpenContactPage();
            }
            FindAndClickMaxItems();
            LastContactEditButtonClick();
            EditLastContact(editContact);
            OpenContactPage();
            FindAndClickMaxItems();
            LastContactEditButtonClick();
            ContactData newcontact = GetCreatedContactData();

            Assert.AreEqual(editContact.Firstname, newcontact.Firstname);
            Assert.AreEqual(editContact.Middlename, newcontact.Middlename);
            Assert.AreEqual(editContact.Lastname, newcontact.Lastname);
            LogOut();

        }

        
        }
    }



