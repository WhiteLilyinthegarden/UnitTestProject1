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
   public class ContactHelper : HelperBase
    {
        private ContactPage contactPage;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
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
            driver.FindElement(By.CssSelector("[href*='edit.php?id=" + id + "']")).Click();
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
            WaitUntilVisible(ContactPage.DeleteContactPage);

        }
    }
}
