using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories.Players;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Players.RegisterPlayer
{
    public class RegisterPlayerHandler : IRequestHandler<RegisterPlayerCommand, IResponse<Unit>>
    {
        private readonly IPlayerRepository _playerRepository;

        public RegisterPlayerHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<IResponse<Unit>> Handle(
            RegisterPlayerCommand request,
            CancellationToken cancellationToken)
        {
            IResponse<Unit> response = new Unit().Success();

            return Task.FromResult(response);
        }
    }
}