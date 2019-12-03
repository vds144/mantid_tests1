using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests.tests
{
    [TestFixture]
    class ProjectCreationTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> GroupDataFromCsvFile()
        {
            List<ProjectData> projects = new List<ProjectData>();
            string[] lines = File.ReadAllLines(@"projects.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                projects.Add(new ProjectData()
                {
                    Name = parts[0],
                    Description = parts[1]
                });
            }
            return projects;
        }

        [Test, TestCaseSource("GroupDataFromCsvFile")]
        public void ProjectCreationTest(ProjectData project)
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            app.Projects.DeleteIfProjectExist(account, project);

            List<ProjectData> oldProjects = app.Projects.GetProjectList(account);

            app.Projects.Create(account, project);

            Assert.AreEqual(oldProjects.Count + 1, app.Projects.GetProjectCount(account));

            List<ProjectData> newProjects = app.Projects.GetProjectList(account);

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}