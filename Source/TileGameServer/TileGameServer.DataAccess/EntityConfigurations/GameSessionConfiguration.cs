using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.EntityConfigurations.Generic;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class GameSessionConfiguration : BaseEntityConfiguration<GameSession>
    {
        public override void Configure(EntityTypeBuilder<GameSession> builder)
        {
            base.Configure(builder);

            builder.HasKey(gameSession => gameSession.Id);
            builder.Property(gameSession => gameSession.Status).IsRequired();
            builder.Property(gameSession => gameSession.CreationDate).IsRequired();
        }
    }
}