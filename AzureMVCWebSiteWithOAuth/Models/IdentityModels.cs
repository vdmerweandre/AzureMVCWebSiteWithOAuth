using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AzureMVCWebSiteWithOAuth.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string HomeTown { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class ContactsDbContext : IdentityDbContext<ApplicationUser>
    {
        public ContactsDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<AzureMVCWebSiteWithOAuth.Models.ContactViewModel> Contacts { get; set; }
    }
}