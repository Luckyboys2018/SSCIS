using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MailService
{
    /// <summary>
    /// Configuraiton class form mail service
    /// </summary>
    public class MailServiceConfiguration
    {
        public int PORT { get; set; }
        public int TIMEOUT { get; set; }

        public string HOST { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }

        public string SENDER { get; set; }
        public string TEST_SUBJECT { get; set; }
        public string TEST_MESSAGE { get; set; }

        /// <summary>
        /// Converts current instance and its values to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Parses JSON representation of configuration to class
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static MailServiceConfiguration ParseJSON(string json)
        {
            return JsonConvert.DeserializeObject<MailServiceConfiguration>(json);
        }

    }

}
