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
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            OpenGroupsPage();
            if (FindMaxItem() == null)
            {
                GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
                CreateNewGroup(group);
                OpenGroupsPage();
            }

            int id = FindAndClickMaxItems();
            DeleteLastGroup();
            OpenGroupsPage();
            Assert.IsTrue(IsIdDeleted(id, "value"));
 

            LogOut();

        }
        
    }
}
