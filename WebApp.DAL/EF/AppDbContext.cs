using System;
using WebApp.Domain;
using WebApp.Domain.Identity;
using Base.DAL.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.DAL.EF
{
    public class AppDbContext : BaseDbContext<AppUser, AppRole, AppUserRole, IdentityUserClaim<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>, AppTranslation, AppTranslationString>
    {
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = default!;
        public DbSet<Picture> Pictures { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Provider> Providers { get; set; } = default!;
        public DbSet<Status> Statuses { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This will remove cascade delete and everything else that is done in BaseDbContext
            base.OnModelCreating(modelBuilder);

            // Prevent Entity Framework from creating multiple foreign keys.
            // Use existing TranslationStringId.
            modelBuilder
                .Entity<AppTranslation>()
                .HasOne(translation => translation.TranslationString)
                .WithMany(translationString => translationString!.Translations)
                .HasForeignKey(translation => translation.TranslationStringId);
            
            // Enable cascade for Category -> Products
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(category => category!.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Location -> Orders
            modelBuilder
                .Entity<Order>()
                .HasOne(order => order.Location)
                .WithMany(location => location!.Orders)
                .HasForeignKey(order => order.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Order -> OrderProducts
            modelBuilder
                .Entity<OrderProduct>()
                .HasOne(orderProduct => orderProduct.Order)
                .WithMany(order => order!.OrderProducts)
                .HasForeignKey(orderProduct => orderProduct.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Order -> Transactions
            modelBuilder
                .Entity<Transaction>()
                .HasOne(transaction => transaction.Order)
                .WithMany(order => order!.Transactions)
                .HasForeignKey(transaction => transaction.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Product -> Pictures
            modelBuilder
                .Entity<Picture>()
                .HasOne(picture => picture.Product)
                .WithMany(product => product!.Pictures)
                .HasForeignKey(picture => picture.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Provider -> Locations
            modelBuilder
                .Entity<Location>()
                .HasOne(location => location.Provider)
                .WithMany(provider => provider!.Locations)
                .HasForeignKey(location => location.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enable cascade for Status -> Orders
            modelBuilder
                .Entity<Order>()
                .HasOne(order => order.Status)
                .WithMany(status => status!.Orders)
                .HasForeignKey(order => order.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}