using AdapterImec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdapterImec.Repository.Configuration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.MessageId).HasColumnName("MessageId").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.CustomerScheme).HasColumnName("CustomerScheme").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.CustomerValue).HasColumnName("CustomerValue").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.ProvidingCompanyScheme).HasColumnName("ProvidingCompanyScheme").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.ProvidingCompanyValue).HasColumnName("ProvidingCompanyValue").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.MessageType).HasColumnName("MessageType").HasColumnType("varchar").HasMaxLength(20);
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").HasColumnType("timestamp");
            builder.Property(x => x.Creator).HasColumnName("Creator").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.DateReceived).HasColumnName("DateReceived").HasColumnType("timestamp");
            builder.Property(x => x.FileContent).HasColumnName("FileContent").HasColumnType("text");
        }
    }
}
