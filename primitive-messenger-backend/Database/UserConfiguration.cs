using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace primitive_messenger_backend.Database
{
    public class UserConfiguration : IEntityTypeConfiguration<Models.User>
    {
        public void Configure(EntityTypeBuilder<Models.User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasAlternateKey(u => u.Phone);
            builder.Property(t => t.Password)
                    .IsRequired();
        }
    }
}
