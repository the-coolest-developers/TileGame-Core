using System;
using System.Threading.Tasks;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Extensions;
using TileGameServer.Infrastructure.Models;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.Infrastructure.Models.Dto.Responses.Menu;

namespace TileGameServer.Services
{
    public class MenuService : IMenuService
    {
        public Task<IResponse<CreateGameResponse>> CreateGame(Guid userId)
        {
            IResponse<CreateGameResponse> result = new CreateGameResponse().GetResponse(ResponseStatus.Success);
            return Task.FromResult(result);
        }

        public Task<IResponse<JoinGameResponse>> JoinGame(Guid userId, Guid sessionId)
        {
            IResponse<JoinGameResponse> result = new JoinGameResponse().GetResponse(ResponseStatus.Success);
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