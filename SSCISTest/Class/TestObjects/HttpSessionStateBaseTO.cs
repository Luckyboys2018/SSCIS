using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSCISTest.Class.TestObjects
{
    public class HttpSessionStateBaseTO : HttpSessionStateBase
    {
        public HttpSessionStateBaseTO()
        {
        }
        public bool ContainsKey(object key)
        {
            foreach (var k in Keys)
            {
                if (k.Equals(key)) return true;
            }
            return false;
        }
    }
}
