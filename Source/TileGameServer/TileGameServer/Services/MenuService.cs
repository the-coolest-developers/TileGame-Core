using System;
using System.Threading.Tasks;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.Infrastructure.Models.Dto.Responses.Menu;

namespace TileGameServer.Services
{
    public class MenuService : IMenuService
    {
        public Task<IResponse<CreateGameResponse>> CreateGame(Guid userId)
        {
            IResponse<CreateGameResponse> result = Response<CreateGameResponse>.Success(new CreateGameResponse());
            return Task.FromResult(result);
        }

        public Task<IResponse<JoinGameResponse>> JoinGame(Guid userId, Guid sessionId)
        {
            IResponse<JoinGameResponse> result = Response<JoinGameResponse>.Success(new JoinGameResponse());
            return Task.FromResult(result);
        }

        public Task<IResponse<Empty>> LeaveGame(Guid userId)
        {
            IResponse<Empty> result = EmptyResponse.FromStatus(ResponseStatus.Success);
            return Task.FromResult(result);
        }

        public Task<IResponse<Empty>> QuitGame(Guid userId)
        {
            IResponse<Empty> result = EmptyResponse.FromStatus(ResponseStatus.Success);
            return Task.FromResult(result);
        }
    }
}