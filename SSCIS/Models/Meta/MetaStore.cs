using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCIS.Models.Meta
{
    public class MetaStore
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Hash { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
    }
}