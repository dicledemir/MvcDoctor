﻿using Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DoctorContext: IdentityDbContext<Person>
    {

        public DoctorContext() : base("DoctorContext")
		{

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Write> Writes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<WriteComment> WriteComments { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Write>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Contact>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Notification>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<WriteComment>()
                .HasKey(x => x.CommentId);
            modelBuilder.Entity<Appointment>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Write>()
                .HasRequired(x => x.Category);
                 
        }







    }
}