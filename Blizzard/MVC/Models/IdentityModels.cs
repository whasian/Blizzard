using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

namespace Blizzard.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    interface ICustomPrincipal : IPrincipal
    {
        bool IsAdmin { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public bool IsAdmin { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public bool IsAdmin { get; set; }
    }
}