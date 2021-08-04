create table if not exists "game"."GameSessionStatuses"
(
	"Id" uuid,
	"GameSessionID" uuid,
	"PlayersCount" smallint
);