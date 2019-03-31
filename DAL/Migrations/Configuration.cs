namespace DAL.Migrations
{
    using Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DoctorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(DAL.DoctorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
             
            //Person p1 = new Person();
            //p1.Email = "admin@a.com";
            //p1.UserName = "admin@a.com";
            ///* p1.ReportsToEmail = "admin@a.com";*///kendisine rapor veriyor


            //IdentityRole role = new IdentityRole();//yönetim kýsmý için kýsýtlama yapabilir yada yapmayabiliriz
            //role.Name = "Administrator";
            //context.SaveChanges();
            //context.Roles.Add(role);
            ////bir kullanýcý var bir rol var bunlarý baglamamýz lazm
            //UserStore<Person> personStore = new UserStore<Person>(context);

            //UserManager<Person> personManager = new UserManager<Person>(personStore);
            //personManager.Create(p1, "Aa123456!");
            //personManager.AddToRoles(p1.Id, "Administrator");

            //context.SaveChanges();
             
        }
    }
}
