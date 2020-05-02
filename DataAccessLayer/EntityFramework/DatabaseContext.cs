using DataAccessLayer.Migrations;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Services> Services { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<ProjectsDone> ProjectsDone { get; set; }

        public DatabaseContext()
        {
            //Database.SetInitializer(new MyInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>("DatabaseContext"));
        }
    }
}
