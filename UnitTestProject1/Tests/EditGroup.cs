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
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Group.OpenGroupsPage();
            GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
            GroupData editGroup = new GroupData("Edit Group") { Header = "Edit Header", Footer = "Edit Footer" };
            if (app.Helper.FindMaxItem() == null)
            {
                app.Group.CreateNewGroup(group);
                app.Group.OpenGroupsPage();
            }
            app.Helper.FindAndClickMaxItems();
            app.Group.OpenLastCreatedLastGroup();
            app.Group.EditLastGroup(editGroup);
            app.Group.OpenGroupsPage();
            app.Helper.FindAndClickMaxItems();
            app.Group.OpenLastCreatedLastGroup();

            GroupData newgroup = app.Group.GetGroupDataFromForm();
            Assert.AreEqual(editGroup.Name, newgroup.Name);
            if (editGroup.Header != null)
                Assert.AreEqual(editGroup.Header, newgroup.Header);
            if (editGroup.Footer != null)
                Assert.AreEqual(editGroup.Footer, newgroup.Footer);


            app.Auth.LogOut();

        }



    }
}



