create table if not exists "game"."SessionPlayers"
(
    "Id"            uuid primary key,
    "GameSessionId" uuid not null,

    constraint "SessionPlayers_GameSessionId_fkey" foreign key ("GameSessionId") references "game"."GameSessions" ("Id"),
    constraint "SessionPlayers_SessionPlayerId_fkey" foreign key ("Id") references "game"."Players" ("Id")
);
