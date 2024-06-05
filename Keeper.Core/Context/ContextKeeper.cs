using Keeper.Core.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Row.AutoGenerator;

namespace Keeper.Core.Context
{
    public class ContextKeeper() : RowDbContext(StageDb, "setting.json")
    {
        private const string StageDb = "Server=tcp:row.database.windows.net;Database=row-db;Persist Security Info=True;User ID=rowadmin;Password=bkSpA2rAxTdKGct;MultipleActiveResultSets=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthCode> UserAuthCode { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRoleAccess> UserRoleAccess { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationBranch> OrganizationBranches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("new-keeper");

            modelBuilder.Entity<User>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<UserAuthCode>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Organization>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<OrganizationBranch>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
