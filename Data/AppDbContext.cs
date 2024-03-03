using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;

namespace WebUI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FClass> FClasses { get; set; }
        public DbSet<LClass> LClasses { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<BestScore> BestScores { get; set; }
        public DbSet <Special> Specials { get; set; }
        public DbSet <Actques> Actqueses { get; set; }
        public DbSet <AdresParam> AdresParams { get; set; }
        public DbSet <Ques> Queses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Diğer yapılandırmalar burada yapılır
            base.OnModelCreating(modelBuilder);

            // Özel yapılandırmaları buraya ekleyin
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

    }
}