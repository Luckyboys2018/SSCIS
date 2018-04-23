using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSCIS.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Class
{
    [TestClass]
    public class SessionHashGeneratorTest
    {
        [TestMethod]
        public void TestGenerateHash()
        {
            SessionHashGenerator gen = new SessionHashGenerator();
            ArrayList dictionary = new ArrayList();
            string hash = null;
            bool result = false;
            hash = gen.GenerateHash();
            dictionary.Add(hash);
            for (int i = 0; i < 99; i++)
            {
                hash = gen.GenerateHash();
                if (dictionary.Contains(hash))
                {
                    result = true;
                }
                dictionary.Add(hash);
            }
            Assert.IsFalse(result);
        }
    }
}
