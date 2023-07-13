using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preorder.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preorder.Data.Configurations
{
    internal class sr_headerConfiguration : IEntityTypeConfiguration<sr_header>
    {
        public void Configure(EntityTypeBuilder<sr_header> builder)
        {

            builder.HasKey(x => x.id);
        }
    }
}
