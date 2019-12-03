using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ApplicationManager : TestBase
    {
        protected IWebDriver driver;
        public string baseURL;
        public FtpHelper Ftp { get; set; }
        public ProjectManagementHelper Projects { get; set; }
        public LoginHelper Auth { get; set; }
        public ManagementMenuHelper Menu { get; private set; }
        public NavigationHelper NavigateTo { get; private set; }
        public RegistrationHelper Registration { get; private set; }
        public APIHelper API { get; set; }


        private new static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.22.1";
            Registration = new RegistrationHelper(this);
            //Ftp = new FtpHelper(this);
            Projects = new ProjectManagementHelper(this);
            Auth = new LoginHelper(this);
            Menu = new ManagementMenuHelper(this);
            NavigateTo = new NavigationHelper(this, baseURL);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
                newInstance.driver.Url = "http://localhost/mantisbt-2.22.1/login_page.php";
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
    }
}
