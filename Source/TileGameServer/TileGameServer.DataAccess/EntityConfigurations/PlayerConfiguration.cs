using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.EntityConfigurations;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class PlayerConfiguration : BaseEntityConfiguration<Player>
    {
        public override void Configure(EntityTypeBuilder<Player> builder)
        {
            base.Configure(builder);

            builder.Property(player => player.Nickname).IsRequired().HasMaxLength(50);
        }
    }
}