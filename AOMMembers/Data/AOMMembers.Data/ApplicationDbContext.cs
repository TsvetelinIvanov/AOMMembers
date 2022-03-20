using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AOMMembers.Data.Common.Models;
using AOMMembers.Data.Models;

namespace AOMMembers.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Citizen> Citizens { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Qualification> Qualifications { get; set; }

        public DbSet<Career> Careers { get; set; }

        public DbSet<WorkPosition> WorkPositions { get; set; }

        public DbSet<Relationship> Relationships { get; set; }

        public DbSet<PartyPosition> PartyPositions { get; set; }

        public DbSet<PartyMembership> PartyMemberships { get; set; }

        public DbSet<MaterialState> MaterialStates { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<PublicImage> PublicImages { get; set; }

        public DbSet<MediaMaterial> MediaMaterials { get; set; }

        public DbSet<LawState> LawStates { get; set; }

        public DbSet<LawProblem> LawProblems { get; set; }

        public DbSet<SocietyHelp> SocietyHelps { get; set; }

        public DbSet<SocietyActivity> SocietyActivities { get; set; }

        public DbSet<Worldview> Worldviews { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Setting> Settings { get; set; }
        
        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            builder.Entity<Member>()
                .HasOne(m => m.ApplicationUser)
                .WithOne(au => au.Member)
                .HasForeignKey<Member>(m => m.ApplicationUserId);

            builder.Entity<Member>()
                .HasOne(m => m.Citizen)
                .WithOne(c => c.Member)
                .HasForeignKey<Member>(m => m.CitizenId);

            builder.Entity<Member>()
                .HasOne(m => m.PublicImage)
                .WithOne(pi => pi.Member)
                .HasForeignKey<Member>(m => m.PublicImageId);

            builder.Entity<Citizen>()
                .HasOne(c => c.Education)
                .WithOne(e => e.Citizen)
                .HasForeignKey<Citizen>(c => c.EducationId);
            
            builder.Entity<Citizen>()
               .HasOne(c => c.Career)
               .WithOne(c => c.Citizen)
               .HasForeignKey<Citizen>(c => c.CareerId);

            builder.Entity<Citizen>()
               .HasOne(c => c.MaterialState)
               .WithOne(ms => ms.Citizen)
               .HasForeignKey<Citizen>(c => c.MaterialStateId);

            builder.Entity<Citizen>()
               .HasOne(c => c.LawState)
               .WithOne(ls => ls.Citizen)
               .HasForeignKey<Citizen>(c => c.LawStateId);

            builder.Entity<Citizen>()
               .HasOne(c => c.Worldview)
               .WithOne(w => w.Citizen)
               .HasForeignKey<Citizen>(c => c.WorldviewId);

            EntityIndexesConfiguration.Configure(builder);

            List<IMutableEntityType> entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            IEnumerable<IMutableEntityType> deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (IMutableEntityType deletableEntityType in deletableEntityTypes)
            {
                MethodInfo method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            IEnumerable<IMutableForeignKey> foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (IMutableForeignKey foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                IAuditInfo entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }

    //public class ApplicationDbContext : IdentityDbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    {
    //    }
    //}
}