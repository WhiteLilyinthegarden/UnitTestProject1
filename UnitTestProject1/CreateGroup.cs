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
            OpenHomePage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            OpenGroupsPage();
            GroupData group = new GroupData("New Group") { Header = "Header", Footer = "Footer" };
            CreateNewGroup(group);
            OpenGroupsPage();
            FindAndClickMaxItems();
            OpenLastCreatedLastGroup();
   
            GroupData newgroup = GetGroupDataFromForm();
            Assert.AreEqual(group.Name, newgroup.Name);
            if (group.Header != null)
                Assert.AreEqual(group.Header, newgroup.Header);
            if (group.Footer!= null)
                Assert.AreEqual(group.Footer, newgroup.Footer);

            LogOut();
        }

        

    }
}



