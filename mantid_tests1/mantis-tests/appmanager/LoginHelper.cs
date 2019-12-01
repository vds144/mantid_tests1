using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }
        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            manager.NavigateTo.OpenMainPage();
            Type(By.Id("username"), account.Name);
            driver.FindElement(By.CssSelector(".btn-success")).Click();
            Type(By.Id("password"), account.Password);
            driver.FindElement(By.CssSelector(".btn-success")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//div[@id='navbar-container']/div[2]/ul/li[3]/a/i[2]")).Click();
                driver.FindElement(By.LinkText("выход")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;

        }

        public string GetLoggetUserName()
        {
            String text = driver.FindElement(By.CssSelector("span.user-info")).Text;
            return text;
        }
    }
}

