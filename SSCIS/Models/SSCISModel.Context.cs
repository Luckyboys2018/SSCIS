﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSCIS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SSCISEntities : DbContext
    {
        public SSCISEntities()
            : base("name=SSCISEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Approval> Approval { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<SSCISContent> SSCISContent { get; set; }
        public virtual DbSet<SSCISSession> SSCISSession { get; set; }
        public virtual DbSet<SSCISUser> SSCISUser { get; set; }
        public virtual DbSet<TutorApplication> TutorApplication { get; set; }
        public virtual DbSet<SSCISParam> SSCISParam { get; set; }
        public virtual DbSet<TutorApplicationSubject> TutorApplicationSubject { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Participation> Participation { get; set; }
    }
}
