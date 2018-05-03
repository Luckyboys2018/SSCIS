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
    public class FeedbacksCSVConverterTest
    {
        [TestMethod]
        public void TestConvert()
        {
            Participation p1 = new Participation() { ID = 1, EventID = 1 };
            Participation p2 = new Participation() { ID = 2, EventID = 2 };
            Subject s1 = new Subject() { ID = 1, Code = "PPA" };
            Subject s2 = new Subject() { ID = 2, Code = "MAT" };
            Event e1 = new Event() { ID = 1, TimeFrom = new DateTime(2018, 01, 02, 10, 0, 0), TimeTo = new DateTime(2018, 01, 02, 11, 0, 0), Subject = s1 };
            Event e2 = new Event() { ID = 2, TimeFrom = new DateTime(2018, 01, 02, 13, 0, 0), TimeTo = new DateTime(2018, 01, 02, 14, 0, 0), Subject = s2 };
            Feedback f1 = new Feedback() { ID = 1, ParticipationID = 1, Text = "Text1" };
            Feedback f2 = new Feedback() { ID = 2, ParticipationID = 2, Text = "Text2" };

            List<Feedback> feedbacks = new List<Feedback>();
            feedbacks.Add(f1);
            feedbacks.Add(f2);

            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            dbMock.Setup(m => m.Participation.Find(1)).Returns(p1);
            dbMock.Setup(m => m.Participation.Find(2)).Returns(p2);
            dbMock.Setup(m => m.Subject.Find(1)).Returns(s1);
            dbMock.Setup(m => m.Subject.Find(2)).Returns(s2);
            dbMock.Setup(m => m.Event.Find(1)).Returns(e1);
            dbMock.Setup(m => m.Event.Find(2)).Returns(e2);

            FeedbacksCSVConverter converter = new FeedbacksCSVConverter();
            string csv = converter.Convert(feedbacks, dbMock.Object);

            Assert.AreEqual("Datum|Od|Do|Předmět|Text\n2.1.2018|10:00|11:00|PPA|Text1\n2.1.2018|13:00|14:00|MAT|Text2\n", csv);
        }

        [TestMethod]
        public void TestFeedbackToString()
        {
            Event e = new Event();
            e.TimeFrom = new DateTime(2018, 01, 02, 10, 0, 0);
            e.TimeTo = new DateTime(2018, 01, 02, 11, 0, 0);
            e.Subject = new Subject() { Code = "PPA" };
            Feedback feedback = new Feedback() { Text = "Abcdefgh." };
            string str = new FeedbacksCSVConverter().FeedbackToString(feedback, e);
            Assert.AreEqual("2.1.2018|10:00|11:00|PPA|Abcdefgh.", str);
        }
    }
}
