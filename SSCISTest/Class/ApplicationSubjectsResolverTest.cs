using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSCIS.Class;
using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Class
{
    [TestClass]
    public class ApplicationSubjectsResolverTest
    {
        [TestMethod]
        public void TestResolveSubjects()
        {
            ApplicationSubjectsResolver resolver = new ApplicationSubjectsResolver();

            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            dbMock.Setup(m => m.Subject.Find(1)).Returns(new Subject() { ID = 1 });
            dbMock.Setup(m => m.Subject.Find(3)).Returns(new Subject() { ID = 3 });
            dbMock.Setup(m => m.Subject.Find(5)).Returns(new Subject() { ID = 5 });

            List<TutorApplicationSubject> subjects = resolver.ResolveSubjects("1 3 5;2 4 1", dbMock.Object);

            Assert.AreEqual(1, subjects[0].Subject.ID);
            Assert.AreEqual(3, subjects[1].Subject.ID);
            Assert.AreEqual(5, subjects[2].Subject.ID);

            Assert.AreEqual(byte.Parse("2"), subjects[0].Degree);
            Assert.AreEqual(byte.Parse("4"), subjects[1].Degree);
            Assert.AreEqual(byte.Parse("1"), subjects[2].Degree);
        }
    }
}
