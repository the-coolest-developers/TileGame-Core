create table if not exists "GameSessions" 
(
	"Id" uuid primary key,
	"Status" smallint,
	"CreationDate" timestamp,
	"Capacity" smallint
);