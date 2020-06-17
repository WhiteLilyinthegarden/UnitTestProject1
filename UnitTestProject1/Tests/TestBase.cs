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


        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = ApplicationManager.GetInstance();
            app.Navigation.OpenHomePage();
        }



        /*   [TearDown]
           public void TeardownTest()
           {
               app.Stop();
             /*  try
               {
                   driver.Quit();
               }
               catch (Exception)
               {
                   // Ignore errors if unable to close the browser
               }
               Assert.AreEqual("", verificationErrors.ToString()); */
        //}


    }


}