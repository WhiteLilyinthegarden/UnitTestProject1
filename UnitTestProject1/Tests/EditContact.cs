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
        public class EditContact :AuthBase
       {
        

            [Test]
            public void EditcontactTest()
        {
            /*app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Contact.OpenContactPage();*/
            ContactData contact = new ContactData("FM", "MN", "LN");
            ContactData editContact = new ContactData("EditFM", "EditMN", "EditLN");
            if (app.Helper.FindMaxItem() == null)
            {
                app.Contact.AddNewContact(contact);
                app.Contact.OpenContactPage();
            }
            app.Helper.FindAndClickMaxItems();
            app.Contact.LastContactEditButtonClick();
            app.Contact.EditLastContact(editContact);
            app.Contact.OpenContactPage();
            app.Helper.FindAndClickMaxItems();
            app.Contact.LastContactEditButtonClick();
            ContactData newcontact = app.Contact.GetCreatedContactData();

            Assert.AreEqual(editContact.Firstname, newcontact.Firstname);
            Assert.AreEqual(editContact.Middlename, newcontact.Middlename);
            Assert.AreEqual(editContact.Lastname, newcontact.Lastname);
            app.Auth.LogOut();

        }

        
        }
    }



