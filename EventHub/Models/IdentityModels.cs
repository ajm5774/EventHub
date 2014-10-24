using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace EventHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext()
            : base("Entities")
        {
        }
    }
}