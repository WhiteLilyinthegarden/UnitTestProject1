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
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
           
            OpenContactPage();
            if (FindMaxItem() == null)
            {
                ContactData contact = new ContactData("FM", "MN", "LN");
                AddNewContact(contact);
                OpenContactPage();

            }

            int id = FindAndClickMaxItems();
            DeleteLastContact();
            OpenContactPage();
            Assert.IsTrue(IsIdDeleted(id, "id"));
          
            LogOut();

        }

    }    
}

