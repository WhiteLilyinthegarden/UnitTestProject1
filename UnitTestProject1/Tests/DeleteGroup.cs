using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    public class DeleteGroup : TestBase
    {
        [Test]
        public void TheDeleteGroupTest()
        {
            app.Navigation.OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            app.Auth.Login(user);
            app.Group.OpenGroupsPage();
            if (app.Helper.FindMaxItem() == null)
            {
                GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
                app.Group.CreateNewGroup(group);
                app.Group.OpenGroupsPage();
            }

            int id = app.Helper.FindAndClickMaxItems();
            app.Group.DeleteLastGroup();
            app.Group.OpenGroupsPage();
            Assert.IsTrue(app.Helper.IsIdDeleted(id, "value"));


            app.Auth.LogOut();

        }
        
    }
}
