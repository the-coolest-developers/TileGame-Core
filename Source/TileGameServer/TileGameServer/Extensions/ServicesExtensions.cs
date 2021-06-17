using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TileGameServer.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDbContextForSqlServer<TDbContext>(this IServiceCollection services,
            string connectionString)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options => { options.UseSqlServer(connectionString); });
        }

        public static void AddDbContextForPostGreSql<TDbContext>(this IServiceCollection services,
            string connectionString)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options => { options.UseNpgsql(connectionString); });
        }
    }
}