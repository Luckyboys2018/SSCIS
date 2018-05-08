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
    public class FeedbackUrlGeneratorTest
    {
        [TestMethod]
        public void TestGenerateURL()
        {
            FeedbackUrlGenerator generator = new FeedbackUrlGenerator();
            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            DateTime date = new DateTime(2018, 09, 10);
            dbMock.Setup(m => m.Event.Find(12)).Returns(new Event() { ID = 12, TimeFrom = date });
            string url = generator.GenerateURL(12, dbMock.Object);
            Assert.IsTrue(url.EndsWith("Feedbacks?code=18091012"));
        }

        [TestMethod]
        public void TestResolveEventID()
        {
            FeedbackUrlGenerator generator = new FeedbackUrlGenerator();
            int? id = generator.ResolveEventID("18091012");
            Assert.AreEqual(12, id);
        }
    }
}
