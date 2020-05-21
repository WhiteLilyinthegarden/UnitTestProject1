using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    public class GroupHelper : HelperBase
    {
        private GroupPage groupPage;

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateNewGroup(GroupData group)
        {
            // Click(OpenGroupsPage.Header)
            // WaitUntilVisible(GroupPage.Header);
            //driver.FindElement(GroupPage.CreateNewGroupButton).Click();
            Click(GroupPage.CreateNewGroupButton);
            FillGroupData(group);
            Click(GroupPage.SaveNewGroupSubmit);
        }
        public void OpenGroupsPage()
        {
            Click(GroupPage.OpenGroupPageButton);
        }

        public GroupData GetGroupDataFromForm()
        {
            string groupName = driver.FindElement(GroupPage.GroupNameTextField).GetAttribute("value");
            string header = driver.FindElement(GroupPage.GroupHeaderTextField).Text;
            string footer = driver.FindElement(GroupPage.GroupFooterTextField).Text;
            return new GroupData(groupName) { Header = header, Footer = footer };


        }
        public void OpenLastCreatedLastGroup()
        {
            //driver.FindElement(By.Name("edit")).Click();
            Click(GroupPage.EditGroupButton);

        }
        public void FillGroupData(GroupData group)
        {
            FillTheField(GroupPage.GroupNameTextField, group.Name);
            if (group.Header != null)
            {
                FillTheField(GroupPage.GroupHeaderTextField, group.Header);
            }
            if (group.Footer != null)
            {
                FillTheField(GroupPage.GroupFooterTextField, group.Footer);
            }
        }
        public void EditLastGroup(GroupData group)
        {
            FillGroupData(group);
            //  driver.FindElement(By.Name("update")).Click();
            Click(GroupPage.SaveEditGroupSubmit);
        }
        public GroupPage GroupPage
        {
            get
            {
                if (groupPage == null)
                {
                    groupPage = new GroupPage();
                }
                return groupPage;
            }
        }
        public void DeleteLastGroup()
        {
            //  driver.FindElement(By.Name("delete")).Click();
            Click(GroupPage.DeleteGroup);
        }
    }
}
