using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class ContactPage
    {
        public By ContactFirstnameTextField { get { return By.Name("firstname"); } }
        public By ContactMiddlenameTextField { get { return By.Name("middlename"); } }
        public By ContactLastnameTextField { get { return By.Name("lastname"); } }
        public By AddNewSubmitButton { get { return By.Name("submit"); } }
        public By EditSubmitButton { get { return By.XPath("(//input[@name='update'])[2]"); } }
        public By OpenContactPage { get { return By.LinkText("home"); } }
        public By AddNewContactPage { get { return By.ClassName("all"); } }
        public By DeleteContactPage { get { return By.XPath("//input[@value='Delete']"); } }

      //  public By SubmitContactButton { get { return By.CssSelector("//input[type=\'submit\']"); } }
    }
}