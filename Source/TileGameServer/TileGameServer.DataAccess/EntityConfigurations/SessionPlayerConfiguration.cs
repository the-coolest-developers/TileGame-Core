using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.EntityConfigurations;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class SessionPlayerConfiguration : BaseEntityConfiguration<SessionPlayer>
    {
        public override void Configure(EntityTypeBuilder<SessionPlayer> builder)
        {
            base.Configure(builder);

            builder.Property(sessionPlayer => sessionPlayer.GameSessionId).IsRequired();

            builder.HasOne(sessionPlayer => sessionPlayer.GameSession)
                .WithMany(gs => gs.Players).IsRequired();
        }
    }
}