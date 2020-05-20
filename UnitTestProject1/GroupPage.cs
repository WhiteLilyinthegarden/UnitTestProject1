using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class GroupPage
    {
        public By GroupNameTextField { get { return By.Name("group_name"); } }
        public By GroupHeaderTextField { get { return By.Name("group_header"); } }
        public By GroupFooterTextField { get { return By.Name("group_footer"); } }
        public By SubmitGroupButton { get { return By.CssSelector("//input[type=\'submit\']"); } }
        public By CreateNewGroupButton { get { return By.Name("new"); } }
        public By SaveNewGroupSubmit { get { return By.Name("submit"); } }
        public By OpenGroupPageButton { get { return By.LinkText("groups"); } }
        public By EditGroupButton { get { return By.Name("edit"); } }
        public By SaveEditGroupSubmit { get { return By.Name("update"); } }
        public By DeleteGroup { get { return By.Name("delete"); } }
    }
}
