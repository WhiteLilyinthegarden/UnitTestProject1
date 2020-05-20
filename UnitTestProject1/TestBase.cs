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
    public class TestBase
    {
        private LoginPage loginPage;
        private GroupPage groupPage;
        private ContactPage contactPage;
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private StringBuilder verificationErrors;
       // private string baseURL;
        protected bool acceptNextAlert = true;

        [SetUp]

        public void SetupTest()
        {
            driver = new FirefoxDriver(@"C:\distr");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
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

        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null)
                {
                    loginPage = new LoginPage();
                }
                return loginPage;
            }
        }
        public GroupPage GroupPage
        {
            get
            {
                if (groupPage  == null)
                {
                    groupPage = new GroupPage();
                }
                return groupPage;
            }
        }
        public ContactPage ContactPage
        {
            get
            {
                if (contactPage == null)
                {
                    contactPage = new ContactPage();
                }
                return contactPage;
            }
        }


        public void WaitUntilVisible(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 3));
            wait.Message = "Element with locator '" + locator + "' was not visible in 3 seconds";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitUntilClickable(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2));
            wait.Message = "Element with locator '" + locator + "' was not clickable in 2 seconds";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static Func<IWebDriver, IWebElement> Condition(By locator)
        {
            return (driver) =>
            {
                var element = driver.FindElements(locator).FirstOrDefault();
                return element != null && element.Displayed && element.Enabled ? element : null;
            };
        }

        protected void Click(By locator)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(Condition(locator)).Click();
           /* WaitUntilClickable(locator);
            driver.FindElement(locator).Click(); альтернативный вариант*/ 
        }

        public void LogOut()
        {
            // driver.FindElement(By.LinkText("Logout")).Click();
            Click(LoginPage.SubmitLogOutButton);
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

        public void FillTheField(By location, string value)
        {
            driver.FindElement(location).Click();
            driver.FindElement(location).Clear();
            driver.FindElement(location).SendKeys(value);
        }

        public void OpenGroupsPage()
        {
            Click(GroupPage.OpenGroupPageButton);
        }

        public void Login(AccountData user)
        {
            FillTheField(LoginPage.UserTextField, user.Username);
            FillTheField(LoginPage.PasswordTextField, user.Password);
            Click(LoginPage.SubmitLoginButton);

        }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/addressbook/group.php");

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

        public ContactData GetCreatedContactData()
        {

            string contactFirstname = driver.FindElement(ContactPage.ContactFirstnameTextField).GetAttribute("value");
            string contactMiddlename = driver.FindElement(ContactPage.ContactMiddlenameTextField).GetAttribute("value");
            string contactLastname = driver.FindElement(ContactPage.ContactLastnameTextField).GetAttribute("value");
            return new ContactData(contactFirstname, contactMiddlename, contactLastname);
        }


        public void AddNewContact(ContactData contact)
        {
            //driver.FindElement(By.ClassName("all")).Click();
            Click(ContactPage.AddNewContactPage);
            FillTheField(ContactPage.ContactFirstnameTextField, contact.Firstname);
            FillTheField(ContactPage.ContactMiddlenameTextField, contact.Middlename);
            FillTheField(ContactPage.ContactLastnameTextField, contact.Lastname);

            // driver.FindElement(ContactPage.AddNewSubmitButton).Click();
            Click(ContactPage.AddNewSubmitButton);
        }

        public void LastContactEditButtonClick()
        {
            string id = FindMaxItem();
            driver.FindElement(By.CssSelector("[href*='edit.php?id=" + id +"']")).Click();
        }

        public void EditLastContact(ContactData contact)
        {
            FillTheField(ContactPage.ContactFirstnameTextField, contact.Firstname);
            FillTheField(ContactPage.ContactMiddlenameTextField, contact.Middlename);
            FillTheField(ContactPage.ContactLastnameTextField, contact.Lastname);
            driver.FindElement(ContactPage.EditSubmitButton).Click();
        }

        public void OpenContactPage()
        {
           //driver.FindElement(By.LinkText("home")).Click();
            Click(ContactPage.OpenContactPage);
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
            //  driver.FindElement(By.Name("delete")).Click();
            Click(GroupPage.DeleteGroup);
        }

        public void DeleteLastContact()
        {
            // driver.FindElement(By.Id(FindMaxById())).Click();

            // driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            // Click(ContactPage.DeleteContactPage);
            driver.FindElement(ContactPage.DeleteContactPage).Click();
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
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