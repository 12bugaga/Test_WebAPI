using Microsoft.EntityFrameworkCore;
using Infrastructure.Entity;
using System.Reflection;
using System.IO;
using System;

namespace Infrastructure
{
    public class AppDataBaseContext : DbContext
    {
        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(u => u.Status).HasDefaultValue("Not approved");
            
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "Иван", Email="qwerty@gmail.com", Password= "4A7D1ED414474E4033AC29CCB8653D9B", KodRole=1 }, //passw = 0000
                new User { Id = 2, UserName = "Николай", Email = "1234@gmail.com", Password = "B59C67BF196A4758191E42F76670CEBA", KodRole = 2, Status = "Approved" } //passw = 1111
                );

            modelBuilder.Entity<RoleUser>().HasData(
                new RoleUser { Id = 1, Role = "User" },
                new RoleUser { Id = 2, Role = "Admin" });

            modelBuilder.Entity<TypeDocument>().HasData(
                new TypeDocument { Id = 1, KodDocumentFile = 1, KodUser = 2, Type = "Passport"});
        }
    }
}
