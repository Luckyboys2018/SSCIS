﻿using SSCIS.Models;
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

        /// <summary>
        /// Starts session for logging user
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="httpSession">session in request context</param>
        public void SessionStart(string login, HttpSessionStateBase httpSession)
        {
            SSCISSession session = new SSCISSession();
            session.SessionStart = DateTime.Now;
            session.Expiration = DateTime.Now.AddSeconds(long.Parse(db.SSCISParam.Where(p => p.ParamKey.Equals("SESSION_LENGTH")).Single().ParamValue));
            session.Hash = hashgenerator.GenerateHash();
            db.SSCISSession.Add(session);
            session.User = db.SSCISUser.Where(u => u.Login.Equals(login)).Single();
            db.SaveChanges();

            httpSession["sessionId"] = session.ID;
            httpSession["role"] = session.User.Role.RoleCode;
            httpSession["hash"] = session.Hash;
            httpSession["login"] = login;
        }

        /// <summary>
        /// Destroys existing session
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="httpSession">session in request conext</param>
        public void SessionDestroy(long sessionId, HttpSessionStateBase httpSession)
        {
            db.SSCISSession.Remove(db.SSCISSession.Where(s => s.ID == sessionId).Single());
            db.SaveChanges();
            httpSession.Remove("sessionId");
            httpSession.Remove("role");
            httpSession.Remove("hash");
        }

        /// <summary>
        /// Verifies if session data stored in request context is correct according to session stored in DB
        /// </summary>
        /// <param name="httpSession">session in request context</param>
        /// <returns>True, if session data is correct, else false</returns>
        public bool VerifySession(HttpSessionStateBase httpSession)
        {
            SSCISSession dbSession = db.SSCISSession.Where(s => s.ID == (long)httpSession["sessionId"]).Single();
            return dbSession.Hash.Equals((string)httpSession["hash"]);
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