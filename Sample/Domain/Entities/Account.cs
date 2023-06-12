﻿namespace Sample.Domain.Entities
{
    public class Account : StrongEntity<Account>
    {
        [Comment("Tên gọi")]
        [MaxLength(100)]
        public string Name { get; set; }

        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Name);
        }
    }
}
