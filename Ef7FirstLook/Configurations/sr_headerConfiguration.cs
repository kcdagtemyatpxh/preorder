using Ef7FirstLook.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ef7FirstLook.Configurations
{
    public class sr_headerConfiguration : IEntityTypeConfiguration<sr_header>
    {
        public void Configure(EntityTypeBuilder<sr_header> builder)
        {
            builder.HasKey(x => x.id);
        }
    }
}
