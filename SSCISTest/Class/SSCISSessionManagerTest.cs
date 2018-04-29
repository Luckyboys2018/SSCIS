using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSCIS.Class;
using SSCIS.Models;
using SSCISTest.Class.TestObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //vytvareni mockObjectu typu SSCISEntities
            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            //nastaveni mockObjectu, aby na volani metody Remove s jakymkoli parametrem typu SSCISSession neprovedl zadnou operaci (nic nevraci, ale nespadne, proto neni Returns)
            dbMock.Setup(m => m.SSCISSession.Remove(It.IsAny<SSCISSession>()));
            //nastaveni mockObjectu, aby na volani metody SaveChanges nic neprovedl
            dbMock.Setup(m => m.SaveChanges());
            //nastaveni mockObjectu, abz na volani metody find s libovolnym parametrem typu int vracel vytvorenou instanci SSCISSession
            dbMock.Setup(m => m.SSCISSession.Find(It.IsAny<int>())).Returns(new SSCISSession());

            SSCISSessionManager session = new SSCISSessionManager(dbMock.Object);

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

        [TestMethod]
        public void TestVerifySession()
        {
            //TODO
            // 1) Vytvorit mock object typu SSCISEntities
            // 2) Vytvorit instanci tridy SSCISSession.SSCISSession a nastavit jeji ID a hash na libovone zvolene cislo a retezet
            // 3) Nastavit chovani pri volani metody Find s parametrem ID, aby vracela vytvorenou instanci tridy SSCISSession
            // 4) Vytvorit instanci SSCISSessionManageru a v konstruktoru predat mock object
            // 5) Vytvorit instanci tridy HttpSessionStateBaseTO a nastavit hodnoty pro klice sessionId a hash na zvolene hodnoty
            // 6) Zavolat metodu VerifySession na sessionManagerem a predat vytvoreny TO a jeji vysledek porovnat s hodnotou true

            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            SSCISSession session = new SSCISSession();
            
            session.ID = 12;
            session.Hash = "123456878";

            dbMock.Setup(m => m.SSCISSession.Find(It.IsAny<int>())).Returns(session);

            SSCISSessionManager sessionMan = new SSCISSessionManager(dbMock.Object);

            HttpSessionStateBaseTO httpSession = new HttpSessionStateBaseTO();
            httpSession["sessionId"] = 12;
            httpSession["hash"] = "123456878";

            Assert.IsTrue(sessionMan.VerifySession(httpSession));
                
        }

        [TestMethod]
        public void TestVerifySession2()
        {
            //TODO
            // 1) Vytvorit mock object typu SSCISEntities
            // 2) Vytvorit instanci tridy SSCISSession a nastavit jeji ID a hash na libovone zvolene cislo a retezet
            // 3) Nastavit chovani pri volani metody SSCISSession.Find s parametrem ID, aby vracela vytvorenou instanci tridy SSCISSession
            // 4) Vytvorit instanci SSCISSessionManageru a v konstruktoru predat mock object
            // 5) Vytvorit instanci tridy HttpSessionStateBaseTO a nastavit hodnoty pro klic sessionId
            // 6) Nastavit hodnotu pro klic hash na jinou hodnotu nez je puvodne zvoleny hash
            // 7) Zavolat metodu VerifySession na sessionManagerem a predat vytvoreny TO a jeji vysledek porovnat s hodnotou false

            Mock<SSCISEntities> dbMock = new Mock<SSCISEntities>();
            SSCISSession session = new SSCISSession();

            session.ID = 12;
            session.Hash = "123456878";

            dbMock.Setup(m => m.SSCISSession.Find(It.IsAny<int>())).Returns(session);

            SSCISSessionManager sessionMan = new SSCISSessionManager(dbMock.Object);

            HttpSessionStateBaseTO httpSession = new HttpSessionStateBaseTO();
            httpSession["sessionId"] = 11;
            httpSession["hash"] = "12345678";

            Assert.IsFalse(sessionMan.VerifySession(httpSession));

        }

    }
}
