using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Techdome.API.Model.Members;

namespace Techdome.API.Model
{
    public class InlineDatabaseContext : DbContext
    {
        /*public InlineDatabaseContext(DbContextOptions<InlineDatabaseContext> options) : base(options)
        {
        }*/
        public DbSet<Member> Config { get; set; }
        public DbSet<UnitMaster> Untmst { get; set; }


        /*protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseInMemoryDatabase("MyDatabase");*/
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=Output.sqlite");
        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("MyDatabase");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitMaster>().Property(e => e.Name).HasMaxLength(50);
            modelBuilder.Entity<UnitMaster>().HasData(new UnitMaster
            {
                Id = 1,
                Name = "KG",
                Group = "KG",
                Desc = "Kilogram"
            },
            new UnitMaster
            {
                Id = 2,
                Name = "MG",
                Group = "KG",
                Desc = "Miligram"
            });
            modelBuilder.Entity<Member>().HasData(new Member
            {
                Id = 1,
                FirstName = "Sahil",
                LastName = "Raj",
                EmailId = "sahilraj@gmail.com",
                Password = "myPassword",
                Role = Roles.Admin
            });

        }
    }
}
