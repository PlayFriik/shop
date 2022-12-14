using Base.Domain.Models;
using Base.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Base.Infrastructure;

public class BaseDbContext<TUser, TRole, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TTranslation, TTranslationString>
    : IdentityDbContext<TUser, TRole, Guid, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    where TUser : BaseUser<TUserRole>
    where TRole : BaseRole<TUserRole>
    where TUserClaim : IdentityUserClaim<Guid>
    where TUserRole : BaseUserRole<TUser, TRole>
    where TUserLogin : IdentityUserLogin<Guid>
    where TRoleClaim : IdentityRoleClaim<Guid>
    where TUserToken : IdentityUserToken<Guid>
    where TTranslation : BaseTranslation<Guid>, new()
    where TTranslationString : BaseTranslationString<Guid, TTranslation>, new()
{
    public DbSet<TTranslation> Translations { get; set; } = default!;
    public DbSet<TTranslationString> TranslationStrings { get; set; } = default!;

    public BaseDbContext(DbContextOptions options) : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Disable cascade delete for everything
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(entityType => entityType.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
            
        modelBuilder
            .Entity<TTranslation>(entityTypeBuilder =>
            {
                // There is actually 1:m between TranslationString and Translation
                entityTypeBuilder
                    .HasOne<TTranslationString>()
                    .WithMany()
                    .HasForeignKey(translation => translation.TranslationStringId)
                    .IsRequired();
                    
                // Only one value allowed per culture/TranslationString
                entityTypeBuilder
                    .HasIndex(translation => new { translation.Culture, translation.TranslationStringId })
                    .IsUnique();
            });

        // Enable cascade for TranslationString -> Translations
        modelBuilder
            .Entity<TTranslationString>()
            .HasMany(translationString => translationString.Translations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // Prevent Entity Framework from creating multiple foreign keys.
        // Use existing UserId and RoleId.
        modelBuilder
            .Entity<TUserRole>()
            .HasOne(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        modelBuilder
            .Entity<TUserRole>()
            .HasOne(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => SaveChangesAsync(true, cancellationToken);

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        // Delete the possible TranslationStrings when entity is deleted.
        // There is no cascade delete from Entity -> TranslationString.
        var entities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Deleted);
            
        foreach (var entity in entities.ToList())
        {
            var found = new List<TTranslationString>();

            foreach (var reference in entity.References.Where(reference => reference.Metadata.FieldInfo != null && (reference.Metadata.FieldInfo.FieldType == typeof(TTranslationString) || reference.Metadata.FieldInfo.FieldType.IsSubclassOf(typeof(BaseTranslationString<Guid, TTranslation>)))))
            {
                var foreignKey = (reference.Metadata as INavigation)?.ForeignKey;
                    
                var foreignKeyPropertyName = foreignKey?.Properties.Single().Name;
                if (foreignKeyPropertyName == null)
                {
                    continue;
                }
                    
                if (reference.TargetEntry == null)
                {
                    // Get the foreign key value
                    var foreignKeyProperty = entity.Entity.GetType().GetProperties().First(property => property.Name == foreignKeyPropertyName);
                    var foreignKeyValue = (Guid) foreignKeyProperty.GetValue(entity.Entity)!;

                    found.Add(new TTranslationString
                    {
                        Id = foreignKeyValue
                    });
                }
                else if (reference.TargetEntry.Entity is TTranslationString translationString)
                {
                    found.Add(translationString);
                }
            }

            // Delete all found TranslationStrings
            if (found.Count >= 1)
            {
                Set<TTranslationString>().RemoveRange(found);
            }
        }
            
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}