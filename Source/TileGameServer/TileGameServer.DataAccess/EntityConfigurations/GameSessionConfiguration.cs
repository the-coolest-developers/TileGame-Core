using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.EntityConfigurations;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class GameSessionConfiguration : BaseEntityConfiguration<GameSession>
    {
        public override void Configure(EntityTypeBuilder<GameSession> builder)
        {
            base.Configure(builder);

            builder.Property(gs => gs.Capacity).IsRequired();
            builder.Property(gs => gs.Status).IsRequired();
            builder.Property(gs => gs.CreationDate).IsRequired();

            builder.HasMany(gs => gs.Players)
                .WithOne(player => player.GameSession).IsRequired();
        }
    }
}