using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infra.Mapping
{
    public class EventHistoryMap : IEntityTypeConfiguration<EventHistory>
    {
        public void Configure(EntityTypeBuilder<EventHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SubscriptionId);

            builder.HasOne(x => x.Subscription).WithMany(x => x.Histories).HasForeignKey(x => x.SubscriptionId);


        }
    }
}
