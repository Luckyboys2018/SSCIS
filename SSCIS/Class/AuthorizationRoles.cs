using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCIS.Class
{
    /// <summary>
    /// Class with list of authorization roles for UCs
    /// </summary>
    public class AuthorizationRoles
    {
        /// <summary>
        /// Administrator role
        /// </summary>
        public const string Administrator = "ADMIN";

        /// <summary>
        /// Tutor role
        /// </summary>
        public const string Tutor = "TUTOR";

        /// <summary>
        /// User role
        /// </summary>
        public const string User = "USER";
    }
}