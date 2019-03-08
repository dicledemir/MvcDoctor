using Entity;
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
                .HasKey(x => x.Id)
                .HasOptional(x=>x.Writes);

            modelBuilder.Entity<Write>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Contact>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Notification>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<WriteComment>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Appointment>()
                .HasKey(x => x.Id);




            modelBuilder.Entity<Write>()
                .HasMany(x => x.WriteComments)
                .WithRequired(x => x.Write)
                .HasForeignKey(x => x.WriteId);


            modelBuilder.Entity<Category>()
               .HasMany(x => x.Writes)
               .WithRequired(x=>x.Category)
               .HasForeignKey(x=>x.CategoryId);


            modelBuilder.Entity<Person>()
                .HasMany(x => x.Appointments)
                .WithRequired(x=>x.person)
                .HasForeignKey(x=>x.PersonId);


            modelBuilder.Entity<Person>()
             .HasMany(x => x.WriteComments)
             .WithRequired(x => x.Person)
             .HasForeignKey(x => x.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany(x => x.Notifications)
                .WithMany();
                

            //modelBuilder.Entity<Language>()
            //.HasMany(x => x.Words)
            //.WithRequired(x => x.Language)
            //.HasForeignKey(x => x.Language_ID);






            //modelBuilder.Entity<Write>()
            //   .HasOptional(x => x.Category);



        }







    }
}
