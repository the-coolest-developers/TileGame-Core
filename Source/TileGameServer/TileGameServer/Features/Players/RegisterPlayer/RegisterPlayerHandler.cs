using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Repositories.Players;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Players.RegisterPlayer
{
    public class RegisterPlayerHandler : IRequestHandler<RegisterPlayerCommand, IResponse<Unit>>
    {
        private readonly IPlayerRepository _playerRepository;

        public RegisterPlayerHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IResponse<Unit>> Handle(
            RegisterPlayerCommand request,
            CancellationToken cancellationToken)
        {
            if (await _playerRepository.ExistsWithIdAsync(request.PlayerId))
            {
                return new Unit().Forbidden();
            }

            var player = new Player
            {
                Id = request.PlayerId,
                Nickname = request.PlayerNickname
            };

            await _playerRepository.CreateAsync(player);
            await _playerRepository.SaveChangesAsync();

            return new Unit().Success();
        }
    }
}