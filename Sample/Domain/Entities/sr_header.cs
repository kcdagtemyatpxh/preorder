using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Domain.Entities
{
    public class sr_header : StrongEntity<sr_header>
    {
        [Comment("Họ tên khách hàng")]
        [MaxLength(100)]
        public string name { get; set; }

        [Comment("địa chỉ")]
        [MaxLength(100)]
        public string address { get; set; }
        
        [Comment("ngày giao hàng")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime deliveryDate { get; set; }

        public override void Configure(EntityTypeBuilder<sr_header> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.name).IsUnique();
            builder.HasIndex(x => x.address);
            builder.HasIndex(x => x.deliveryDate);
        }
    }
}
