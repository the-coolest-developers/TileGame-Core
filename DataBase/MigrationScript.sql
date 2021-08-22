alter table "game"."Players"
    drop constraint "Players_GameSessionId_fkey",
    drop column "GameSessionId",
    add column "Nickname" varchar(50);

alter table "game"."GameSessions"
    add column "CreatorId" uuid not null default '00000000-0000-0000-0000-000000000000',
    add constraint "GameSessions_CreatorId_fkey" foreign key ("CreatorId") references "game"."Players" ("Id")
