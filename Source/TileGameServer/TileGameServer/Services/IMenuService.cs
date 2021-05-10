using System;
using System.Threading.Tasks;

namespace TileGameServer.Services
{
    public interface IMenuService
    {
        /// <summary>
        /// Creates a new game session
        /// </summary>
        /// <param name="userId">The creator of the session</param>
        /// <returns></returns>
        public Task CreateGame(Guid userId);
        
        /// <summary>
        /// Attaches the user to the game session
        /// </summary>
        /// <param name="userId">The user who joins the session</param>
        /// <param name="sessionId">Game session id the user should be attached to</param>
        /// <returns></returns>
        public Task JoinGame(Guid userId, Guid sessionId);
        
        /// <summary>
        /// Detaches the user from the game session
        /// </summary>
        /// <param name="userId">The user who leaves the session</param>
        /// <returns></returns>
        public Task LeaveGame(Guid userId);
        
        /// <summary>
        /// Quits an existing game session
        /// </summary>
        /// <param name="userId">The creator of the session</param>
        /// <returns></returns>
        public Task QuitGame(Guid userId);
    }
}