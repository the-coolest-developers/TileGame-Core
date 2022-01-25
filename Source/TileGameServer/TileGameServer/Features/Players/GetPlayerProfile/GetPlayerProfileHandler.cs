using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories.Players;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Players.GetPlayerProfile;

public class GetPlayerProfileHandler : IRequestHandler<GetPlayerProfileQuery, IResponse<GetPlayerProfileResponse>>
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayerProfileHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IResponse<GetPlayerProfileResponse>> Handle(
        GetPlayerProfileQuery request,
        CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetAsync(request.PlayerId);

        if (player == null)
        {
            return new Response<GetPlayerProfileResponse>
            {
                Status = ResponseStatus.Conflict
            };
        }

        var response = new GetPlayerProfileResponse
        {
            Nickname = player.Nickname
        };

        return response.Success();
    }
}