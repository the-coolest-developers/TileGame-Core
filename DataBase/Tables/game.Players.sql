create table if not exists "game"."Players"
(
	"Id" uuid,
	"GameSessionId" uuid, 
	foreign key ("GameSessionId") references "game"."GameSessions"("Id")
);