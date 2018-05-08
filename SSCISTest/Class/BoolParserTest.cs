using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSCIS.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Class
{
    [TestClass]
    public class BoolParserTest
    {
        [TestMethod]
        public void TestParse()
        {
            Assert.IsFalse(BoolParser.Parse("F"));
            Assert.IsFalse(BoolParser.Parse("f"));
            Assert.IsFalse(BoolParser.Parse("0"));
            Assert.IsFalse(BoolParser.Parse("False"));
            Assert.IsFalse(BoolParser.Parse("false"));
            Assert.IsFalse(BoolParser.Parse(null));
            Assert.IsTrue(BoolParser.Parse("T"));
            Assert.IsTrue(BoolParser.Parse("t"));
            Assert.IsTrue(BoolParser.Parse("1"));
            Assert.IsTrue(BoolParser.Parse("True"));
            Assert.IsTrue(BoolParser.Parse("true"));
        }
    }
}
