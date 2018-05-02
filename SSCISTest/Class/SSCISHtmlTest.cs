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
    public class SSCISHtmlTest
    {
        [TestMethod]
        public void TestDisplayForBool()
        {
            Assert.AreEqual("", SSCISHtml.DisplayForBool(null));
            Assert.AreEqual("Ano", SSCISHtml.DisplayForBool(true));
            Assert.AreEqual("Ne", SSCISHtml.DisplayForBool(false));
        }
    }
}
