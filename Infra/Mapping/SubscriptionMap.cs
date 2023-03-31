using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infra.Mapping
{
    public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);
            builder.Property(x => x.StatusId);

            builder.HasOne(x => x.Status).WithMany(x => x.Subscriptions).HasForeignKey(x => x.StatusId);

        }
    }
}
