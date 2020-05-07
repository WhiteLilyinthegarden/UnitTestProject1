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
    public class TestBase
    {

        protected IWebDriver driver;
        private StringBuilder verificationErrors;
       // private string baseURL;
        protected bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {

            driver = new FirefoxDriver(@"C:\distr");
           // baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        public void LogOut()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        public void CreateNewGroup(GroupData group)
        {
            driver.FindElement(By.Name("new")).Click();
            FillGroupData(group);
            driver.FindElement(By.Name("submit")).Click();
        }

        public void FillTheField(string location, string value)
        {
            driver.FindElement(By.Name(location)).Click();
            driver.FindElement(By.Name(location)).Clear();
            driver.FindElement(By.Name(location)).SendKeys(value);
        }

        public void OpenGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void Login(AccountData user)
        {
            FillTheField("user", user.Username);
            FillTheField("pass", user.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/addressbook/group.php");
        }

        public GroupData GetGroupDataFromForm()
        {
            string groupName = driver.FindElement(By.Name("group_name")).GetAttribute("value");
            string header = driver.FindElement(By.Name("group_header")).Text;
            string footer = driver.FindElement(By.Name("group_footer")).Text;
            return new GroupData(groupName) { Header = header, Footer = footer };


        }

        public void OpenLastCreatedLastGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
        }

        public void FillGroupData(GroupData group)
        {
            FillTheField("group_name", group.Name);
            if (group.Header != null)
            {
                FillTheField("group_header", group.Header);
            }
            if (group.Footer != null)
            {
                FillTheField("group_footer", group.Footer);
            }
        }

        public void EditLastGroup(GroupData group)
        {
            FillGroupData(group);
            driver.FindElement(By.Name("update")).Click();
        }

        public ContactData GetCreatedContactData()
        {

            string contactFirstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string contactMiddlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string contactLastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            return new ContactData(contactFirstname, contactMiddlename, contactLastname);
        }


        public void AddNewContact(ContactData contact)
        {
            driver.FindElement(By.ClassName("all")).Click();
            FillTheField("firstname", contact.Firstname);
            FillTheField("middlename", contact.Middlename);
            FillTheField("lastname", contact.Lastname);

            driver.FindElement(By.Name("submit")).Click();
        }

        public void LastContactEditButtonClick()
        {
            string id = FindMaxItem();
            driver.FindElement(By.CssSelector("[href*='edit.php?id=" + id +"']")).Click();
        }

        public void EditLastContact(ContactData contact)
        {
            FillTheField("firstname", contact.Firstname);
            FillTheField("middlename", contact.Middlename);
            FillTheField("lastname", contact.Lastname);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
        }

        public void OpenContactPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }




        public int FindAndClickMaxItems()
        {
            IReadOnlyCollection<IWebElement> text = driver.FindElements(By.Name("selected[]"));
            List<int> value = new List<int>();
            foreach (IWebElement str in text)
            {
                string my_text = str.GetAttribute("value");
                value.Add(Convert.ToInt16(my_text));
            }
            int maxInt = value.Max();
            string max = Convert.ToString(value.Max());
            foreach (IWebElement str1 in text)
            {
                string my_text1 = str1.GetAttribute("value");
                int currentValue = Convert.ToInt16(my_text1);
                if (currentValue == maxInt)
                {
                    str1.Click();
                    return maxInt;
                }
            }
            return maxInt;
        }

        public string FindMaxItem()
        {
            IReadOnlyCollection<IWebElement> text = driver.FindElements(By.Name("selected[]"));
            if (text.Count > 0)
            {
                List<int> id = new List<int>();
                foreach (IWebElement str in text)
                {
                    string my_text = str.GetAttribute("value");
                    id.Add(Convert.ToInt16(my_text));
                }
                string max = Convert.ToString(id.Max());
                return max;
            }
            else return null;
        }

        public void DeleteLastGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        public void DeleteLastContact()
        {
            // driver.FindElement(By.Id(FindMaxById())).Click();
           
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
          
        }

        public bool IsIdDeleted(int idDeleted, string attribute)
        {
            if (IsElementsListEmpty()) { return true; }
            IReadOnlyCollection<IWebElement> text = driver.FindElements(By.Name("selected[]"));
            idDeleted = Convert.ToInt16(idDeleted);
            foreach (IWebElement str in text)
            {
                string currentIdValue = str.GetAttribute(attribute);
                int currentId = Convert.ToInt16(currentIdValue);
                if (idDeleted == currentId)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool IsElementsListEmpty() => !IsElementPresent(By.XPath("//input[@name='selected[]']"));


        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
               
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

    }




}