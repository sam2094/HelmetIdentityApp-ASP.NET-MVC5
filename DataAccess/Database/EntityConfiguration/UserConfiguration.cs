using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Database.EntityConfiguration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users", "dbo");

            Property(e => e.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(true);

            Property(e => e.Phone)
              .HasMaxLength(50)
              .IsUnicode(true);

			Property(e => e.QRCode)
				.IsRequired();

			Property(e => e.AddedDate)
                .IsRequired();
        }
    }
}
