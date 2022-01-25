using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.InSession.Features.GetTileField;

public class GetTileFieldQuery : IRequest<IResponse<GetTileFieldResponse>>
{
    public Guid SessionId { get; init; }
}