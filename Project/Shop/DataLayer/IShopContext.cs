using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer
{
    public interface IShopContext
    {
        int SaveChanges();
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<RelatedProduct> RelatedProducts { get; set; }
        DbSet<Log> Logs { get; set; }

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
    }
}