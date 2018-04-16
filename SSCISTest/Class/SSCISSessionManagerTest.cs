using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSCIS.Class;
using SSCISTest.Class.TestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSCISTest.Class
{
    [TestClass]
    public class SSCISSessionManagerTest
    {
        [TestMethod]
        public void TestSessionDestroy()
        {
            SSCISSessionManager session = new SSCISSessionManager();

            HttpSessionStateBaseTO httpSession = new HttpSessionStateBaseTO();
            httpSession["sessionId"] = 1;
            httpSession["role"] = 2;
            httpSession["hash"] = "34355677887988533";
            httpSession["userID"] = 1234;

            session.SessionDestroy(1, httpSession);

            Assert.IsFalse(httpSession.ContainsKey("sessionId"));
            Assert.IsFalse(httpSession.ContainsKey("role"));
            Assert.IsFalse(httpSession.ContainsKey("hash"));
            Assert.IsFalse(httpSession.ContainsKey("userID"));
        }
    }
}
