﻿using Keeper.Core.Context.Models;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> ProductDiscounts { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<ProductPurchaseDetail> ProductPurchaseDetails { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("new-keeper");

            modelBuilder.Entity<User>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<UserAuthCode>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Organization>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Category>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Product>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Discount>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<ProductPurchase>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<ProductPurchaseDetail>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Supplier>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Plan>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            modelBuilder.Entity<Subscription>()
                .Property(x => x.Active).HasDefaultValue(true);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
