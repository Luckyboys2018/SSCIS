using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;

namespace MailService
{
    /// <summary>
    /// Class of mail-sending service
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// Configuration for service
        /// </summary>
        private static MailServiceConfiguration Configuration { get; set; }

        /// <summary>
        /// Path of JSON with configuration
        /// </summary>
        private const string CFG_PATH = "\\SSCIS\\MailService\\mail.cfg.json";

        /// <summary>
        /// Contructor
        /// </summary>
        public MailService()
        {
            if (Configuration == null) loadConfiguraiton();
        }

        /// <summary>
        /// Sends a test email message
        /// </summary>
        /// <param name="addressee">addressee's mail</param>
        public static void SendTestMail(string addressee)
        {
            SmtpClient client = new SmtpClient();
            client.Port = Configuration.PORT;
            client.Host = Configuration.HOST;
            client.EnableSsl = true;
            client.Timeout = Configuration.TIMEOUT;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Configuration.LOGIN, Configuration.PASSWORD);

            MailMessage mm = new MailMessage(Configuration.SENDER, addressee, Configuration.TEST_SUBJECT, Configuration.TEST_MESSAGE);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        /// <summary>
        /// Loads service's configuration from JSON file
        /// </summary>
        private static void loadConfiguraiton()
        {
            using (TextReader tr = new StreamReader(CFG_PATH))
            {
                Configuration = MailServiceConfiguration.ParseJSON(tr.ReadToEnd());
            }
        }

    }
}
