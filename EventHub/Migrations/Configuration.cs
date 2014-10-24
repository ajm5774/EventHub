namespace EventHub.Migrations
{
    using EventHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventHub.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventHub.Models.ApplicationDbContext context)
        {
        }
    }
}
