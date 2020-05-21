using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    public class CreateGroup : TestBase
    {     

        [Test]
        public void CreateGroupTest()
        {
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Group.OpenGroupsPage();
            GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
            app.Group.CreateNewGroup(group);
            app.Group.OpenGroupsPage();
            app.Helper.FindAndClickMaxItems();
            app.Group.OpenLastCreatedLastGroup();
   
            GroupData newgroup = app.Group.GetGroupDataFromForm();
            Assert.AreEqual(group.Name, newgroup.Name);
            if (group.Header != null)
                Assert.AreEqual(group.Header, newgroup.Header);
            if (group.Footer!= null)
                Assert.AreEqual(group.Footer, newgroup.Footer);

            app.Auth.LogOut();
        }

        

    }
}



