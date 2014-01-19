namespace AzureMVCWebSiteWithOAuth.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AzureMVCWebSiteWithOAuth.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<AzureMVCWebSiteWithOAuth.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AzureMVCWebSiteWithOAuth.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            AddUserAndRole(context);

            context.Contacts.AddOrUpdate(p => p.Name,
           new Contact
           {
               Name = "Debra Garcia",
               Address = "1234 Main St",
               City = "Redmond",
               State = "WA",
               Zip = "10999",
               Email = "debra@example.com",
           },
            new Contact
            {
                Name = "Thorsten Weinrich",
                Address = "5678 1st Ave W",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "thorsten@example.com",
            },
            new Contact
            {
                Name = "Yuhong Li",
                Address = "9012 State st",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "yuhong@example.com",
            },
            new Contact
            {
                Name = "Jon Orton",
                Address = "3456 Maple St",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "jon@example.com",
            },
            new Contact
            {
                Name = "Diliana Alexieva-Bosseva",
                Address = "7890 2nd Ave E",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "diliana@example.com",
            }
            );
        }

        bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1",
            };
            ir = um.Create(user, "Passw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
