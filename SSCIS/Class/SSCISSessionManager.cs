using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SSCIS.Class
{
    /// <summary>
    /// Session managing class
    /// </summary>
    public class SSCISSessionManager
    {
        private SSCISEntities db = new SSCISEntities();
        private SessionHashGenerator hashgenerator = new SessionHashGenerator();

        public SSCISSessionManager(SSCISEntities dbContext = null)
        {
            if (dbContext == null)
            {
                this.db = new SSCISEntities();
            }
            else
            {
                this.db = dbContext;
            }
        }

        /// <summary>
        /// Starts session for logging user
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="httpSession">session in request context</param>
        public int SessionStart(string login, HttpSessionStateBase httpSession)
        {
            CleanSessions();

            SSCISSession session = new SSCISSession();
            session.SessionStart = DateTime.Now;
            session.Expiration = DateTime.Now.AddSeconds(long.Parse(db.SSCISParam.Where(p => p.ParamKey.Equals(SSCISParameters.SESSION_LENGTH)).Single().ParamValue));
            session.Hash = hashgenerator.GenerateHash();
            db.SSCISSession.Add(session);
            session.User = db.SSCISUser.Where(u => u.Login.Equals(login)).Single();
            db.SaveChanges();

            if (!BoolParser.Parse(db.SSCISParam.Where(p => p.ParamKey.Equals(SSCISParameters.WEB_AUTH_ON)).Single().ParamValue))
            {
                httpSession["sessionId"] = session.ID;
                httpSession["role"] = session.User.Role.RoleCode;
                httpSession["hash"] = session.Hash;
                httpSession["login"] = login;
                httpSession["userId"] = session.User.ID;
            }
            return session.ID;
        }

        /// <summary>
        /// Destroys existing session
        /// Session KAPUT
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="httpSession">session in request conext</param>
        public void SessionDestroy(long sessionId, HttpSessionStateBase httpSession)
        {
            db.SSCISSession.Remove(db.SSCISSession.Find(sessionId));
            db.SaveChanges();
            httpSession.Remove("sessionId");
            httpSession.Remove("role");
            httpSession.Remove("hash");
            httpSession.Remove("userID");
        }

        /// <summary>
        /// Verifies if session data stored in request context is correct according to session stored in DB
        /// </summary>
        /// <param name="httpSession">session in request context</param>
        /// <returns>True, if session data is correct, else false</returns>
        public bool VerifySession(HttpSessionStateBase httpSession)
        {
            //SSCISSession dbSession = db.SSCISSession.Where(s => s.ID == (int)httpSession["sessionId"]).Single();
            SSCISSession dbSession = db.SSCISSession.Find(httpSession["sessionId"]);
            if (dbSession.Expiration < DateTime.Now) return false;
            if (httpSession["hash"] != null)
            {
                return dbSession.Hash.Equals((string)httpSession["hash"]);
            }
            return false;
        }

        /// <summary>
        /// Clears expired sessions
        /// </summary>
        public void CleanSessions()
        {
            db.SSCISSession.RemoveRange(db.SSCISSession.Where(x => x.Expiration.CompareTo(DateTime.Now) < 0));
            db.SaveChanges();
        }

    }
}