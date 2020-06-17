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
  public class HelperBase : TestBase
    {
        
        protected WebDriverWait wait;
       // private StringBuilder verificationErrors;
        //private string baseURL;
        protected bool acceptNextAlert = true;
        protected ApplicationManager manager;
        protected IWebDriver driver;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            this.driver = manager.Driver;
        }

        public void WaitUntilVisible(By locator)
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4));
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
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(Condition(locator)).Click();
            /* WaitUntilClickable(locator);
             driver.FindElement(locator).Click(); альтернативный вариант*/
        }

        public void FillTheField(By location, string value)
        {
            driver.FindElement(location).Click();
            driver.FindElement(location).Clear();
            driver.FindElement(location).SendKeys(value);
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
