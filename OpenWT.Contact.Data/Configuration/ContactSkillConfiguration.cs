using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Data.Configuration
{
    public class ContactSkillConfiguration : IEntityTypeConfiguration<ContactSkillEntity>
    {
        public void Configure(EntityTypeBuilder<ContactSkillEntity> builder)
        {
            builder
                .HasKey(bc => new { bc.ContactId, bc.SkillId });
            
            builder
                .HasOne(contactSkillEntity => contactSkillEntity.Contact)
                .WithMany(contactEntity => contactEntity.ContactSkills)
                .HasForeignKey(contactSkillEntity => contactSkillEntity.ContactId);

            builder
                .HasOne(contactSkillEntity => contactSkillEntity.Skill)
                .WithMany(contactEntity => contactEntity.ContactSkills)
                .HasForeignKey(contactSkillEntity => contactSkillEntity.SkillId);
        }
    }
}