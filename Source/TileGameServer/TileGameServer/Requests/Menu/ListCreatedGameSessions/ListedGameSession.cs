﻿using System;

namespace TileGameServer.Requests.Menu.ListCreatedGameSessions
{
    public class ListedGameSession
    {
        public Guid Id { get; set; }
        public string CreatorNickname { get; set; }
        public int Capacity { get; set; }
        public int PlayerAmount { get; set; }
    }
}