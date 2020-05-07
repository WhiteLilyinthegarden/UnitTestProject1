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
    public class EditGroup : TestBase
    {


        [Test]
        public void EditgroupTest()
        {
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);

            OpenGroupsPage();
            GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
            GroupData editGroup = new GroupData("Edit Group") { Header = "Edit Header", Footer = "Edit Footer" };
            if (FindMaxItem() == null)
            {
                CreateNewGroup(group);
                OpenGroupsPage();
            }
            FindAndClickMaxItems();
            OpenLastCreatedLastGroup();
            EditLastGroup(editGroup);
            OpenGroupsPage();
            FindAndClickMaxItems();
            OpenLastCreatedLastGroup();

            GroupData newgroup = GetGroupDataFromForm();
            Assert.AreEqual(editGroup.Name, newgroup.Name);
            if (editGroup.Header != null)
                Assert.AreEqual(editGroup.Header, newgroup.Header);
            if (editGroup.Footer != null)
                Assert.AreEqual(editGroup.Footer, newgroup.Footer);


            LogOut();

        }



    }
}



