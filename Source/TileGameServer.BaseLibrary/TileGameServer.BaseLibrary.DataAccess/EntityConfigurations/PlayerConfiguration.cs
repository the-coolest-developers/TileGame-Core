using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.EntityConfigurations;

namespace TileGameServer.BaseLibrary.DataAccess.EntityConfigurations
{
    public class PlayerConfiguration : BaseEntityConfiguration<SessionPlayer>
    {
        public override void Configure(EntityTypeBuilder<SessionPlayer> builder)
        {
            base.Configure(builder);

            builder.Property(player => player.GameSessionId).IsRequired();

            builder.HasOne(player => player.GameSession)
                .WithMany(gs => gs.Players).IsRequired();
        }
    }
}