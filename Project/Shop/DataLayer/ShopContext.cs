using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataLayer
{
    public class SqlServerShopContext : IdentityDbContext<Customer,CustomerRole,int>, IShopContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ShopDB;Integrated Security=true;").UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(p=>p.UserId);
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(p => new { p.UserId, p.RoleId });
            
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(p => new { p.UserId });

            modelBuilder.Ignore<BaseEntity>();
            modelBuilder.Ignore<BaseLog>();

            foreach (var EntityType in modelBuilder.Model.GetEntityTypes())
            {
                var prop = EntityType.FindProperty("CreateOn");
                if(prop!=null)
                {
                    prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    prop.SqlServer().DefaultValueSql = "GetDate()";
                }
                 prop = EntityType.FindProperty("UpdateOn");
                if (prop != null)
                {
                    prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
                    prop.SqlServer().DefaultValueSql = "GetDate()";
                }
            }

        }
        //public DbSet<Seller> Sellers { get; set; }
        //public DbSet<Seller> SellerRolls { get; set; }
       // public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RelatedProduct> RelatedProducts { get; set; }
        public DbSet<Log> Logs { get; set; }
    }



    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(2000).IsRequired();

        }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Sku).HasMaxLength(50).IsRequired();

        }
    }
    
}
