using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Data.Context
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }
        
        #region Entities

        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public DbSet<ContactSkillEntity> ContactSkills { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Retrieve entities configuration
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Make sure every delete is restricted.
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}