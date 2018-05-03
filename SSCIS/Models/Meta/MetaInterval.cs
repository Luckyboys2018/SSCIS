using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSCIS.Models.Meta
{
    public class MetaInterval
    {
        [DisplayName("Od")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DisplayName("Do")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}