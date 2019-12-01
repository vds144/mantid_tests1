using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager) { }

        public void OpenManagementMenu()
        {
            driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li/a/span[contains(text(),'Управление')]")).Click();
        }

        public void GoToProjectTab()
        {
            OpenManagementMenu();
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/ul/li/a[contains(text(),'Управление проектами')]")).Click();
        }
    }
}
