create table if not exists "game"."GameSessions"
(
	"Id" uuid primary key,
	"Status" smallint,
	"CreationDate" timestamp,
	"Capacity" smallint
);