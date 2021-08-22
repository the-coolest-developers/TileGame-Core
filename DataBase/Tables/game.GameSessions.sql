create table if not exists "game"."GameSessions"
(
    "Id"           uuid      not null primary key,
    "CreatorId"    uuid      not null,
    "Status"       smallint  not null,
    "CreationDate" timestamp not null,
    "Capacity"     smallint  not null,

    --primary key ("Id", "CreatorId"),

    constraint "GameSessions_CreatorId_fkey" foreign key ("CreatorId") references "game"."Players" ("Id")
);
