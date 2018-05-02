using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCIS.Class
{

    /// <summary>
    /// Helper class for building list of subjects on application
    /// </summary>
    public class ApplicationSubjectsResolver
    {
        /// <summary>
        /// Resolves list of subjects of application
        /// </summary>
        /// <param name="data">String with subjects ids and degrees</param>
        /// <param name="db">Db context</param>
        /// <returns>List of subjects</returns>
        public List<TutorApplicationSubject> ResolveSubjects(string data, SSCISEntities db)
        {
            List<TutorApplicationSubject> result = new List<TutorApplicationSubject>();
            string subjectsStr = data.Split(';')[0];
            string degreesStr = data.Split(';')[1];
            string[] subjectsArr = subjectsStr.Split(' ');
            string[] degreesArr = degreesStr.Split(' ');

            for (int i = 0; i < degreesArr.Length; i++)
            {
                if (subjectsArr[i].Length > 0 && degreesArr[i].Length > 0)
                {
                    result.Add(new TutorApplicationSubject() { Subject = db.Subject.Find(int.Parse(subjectsArr[i])), Degree = byte.Parse(degreesArr[i]) });
                }
            }

            return result;
        }
        
    }
}