using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.InSession.Features.GetTileField;

public class GetTileFieldHandler : IRequestHandler<GetTileFieldQuery, IResponse<GetTileFieldResponse>>
{
    private readonly IInSessionContext _inSessionContext;

    public GetTileFieldHandler(IInSessionContext inSessionContext)
    {
        _inSessionContext = inSessionContext;
    }

    public Task<IResponse<GetTileFieldResponse>> Handle(
        GetTileFieldQuery request,
        CancellationToken cancellationToken)
    {
        var session = _inSessionContext.EntitySet<GameSession>()
            .FirstOrDefault(s => s.Id == request.SessionId);

        IResponse<GetTileFieldResponse> result;

        if (session == null)
        {
            result = new Response<GetTileFieldResponse>
            {
                Status = ResponseStatus.Conflict
            };

            return Task.FromResult(result);
        }

        var response = new GetTileFieldResponse
        {
            PlacedTiles = session.TileField.GetPlacedTiles()
        };
        result = response.Success();

        return Task.FromResult(result);
    }
}