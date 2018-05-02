using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCIS.Models.Meta
{
    public class MetaTutorApplication
    {
        public TutorApplication Application { get; set; }

        public int CountOfSubjects { get; set; }

        public string SubjectData { get; set; }

        public List<TutorApplicationSubject> ApplicationSubjects { get; set; }

        public MetaTutorApplication()
        {
            this.ApplicationSubjects = new List<TutorApplicationSubject>();
            this.Application = new TutorApplication();
        }
    }
}