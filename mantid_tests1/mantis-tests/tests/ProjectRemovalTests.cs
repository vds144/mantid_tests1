using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Name = "New Project",
            };

            app.Projects.CreateIfNoProjectsExists(account, project);

            List<ProjectData> oldProjects = app.Projects.GetProjectList(account);

            ProjectData toBeRemoved = oldProjects[0];

            app.Projects.Remove(account, toBeRemoved.Id);

            Assert.AreEqual(oldProjects.Count - 1, app.Projects.GetProjectCount(account));

            List<ProjectData> newProjects = app.Projects.GetProjectList(account);

            oldProjects.RemoveAt(0);

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
