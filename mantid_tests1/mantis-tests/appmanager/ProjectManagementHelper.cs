﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        internal void Create(ProjectData project)
        {
            manager.Menu.OpenManagementMenu();
            manager.Menu.GoToProjectTab();
            InitProjectCreation();
            FillProjectData(project);
            SubmitProjectCreation();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("div.widget-toolbox input.btn-primary")).Click();
        }

        private void FillProjectData(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).Clear();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            driver.FindElement(By.Id("project-description")).Clear();
            driver.FindElement(By.Id("project-description")).SendKeys(project.Description);
        }

        private void InitProjectCreation()
        {
            driver.FindElements(By.CssSelector("button.btn.btn-primary"))[0].Click();
        }
        public void CreateIfNoProjectsExists()
        {
            manager.Menu.GoToProjectTab();

            if (!IsElementPresent(By.XPath("//table[1]/tbody/tr")))
            {
                ProjectData project = new ProjectData()
                {
                    Name = "test project to delete",
                    Description = "project description"
                };
                Create(project);
            }
        }
        public void Remove(ProjectData project)
        {
            manager.Menu.GoToProjectTab();
            OpenProjectPage(project.Name);
            RemoveProject();
            SubmitProjectRemoval();
        }

        public void OpenProjectPage(String name)
        {
            driver.FindElement(By.LinkText(name)).Click();
        }

        public void RemoveProject()
        {
            driver.FindElement(By.CssSelector("form#project-delete-form input.btn")).Click();
        }

        public void SubmitProjectRemoval()
        {
            driver.FindElement(By.CssSelector("div.alert-warning .btn")).Click();
        }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> list = new List<ProjectData>();
            manager.Menu.OpenManagementMenu();
            manager.Menu.GoToProjectTab();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector(".table"))[0]
                .FindElements(By.CssSelector("tbody>tr"));
            foreach (IWebElement element in elements)
            {
                list.Add(new ProjectData()
                {
                    Name = element.FindElements(By.CssSelector("td"))[0].Text,
                    Description = element.FindElements(By.CssSelector("td"))[4].Text
                });
            }
            return list;
        }

        public int GetProjectCount()
        {
            manager.Menu.OpenManagementMenu();
            manager.Menu.GoToProjectTab();
            return driver.FindElements(By.CssSelector(".table"))[0]
                .FindElements(By.CssSelector("tbody>tr"))
                .Count();
        }

        public void DeleteIfProjectExist(ProjectData project)
        {
            manager.Menu.GoToProjectTab();

            if (IsElementPresent(By.XPath("//table[1]/tbody/tr/td[1]/a[.='" + project.Name + "']")))
            {
                Remove(project);
            }
        }
    }
}