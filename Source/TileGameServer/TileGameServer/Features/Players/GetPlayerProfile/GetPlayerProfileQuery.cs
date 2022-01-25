using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Players.GetPlayerProfile;

public class GetPlayerProfileQuery : IRequest<IResponse<GetPlayerProfileResponse>>
{
    public Guid PlayerId { get; init; }
}