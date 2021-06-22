create table if not exists "GameSessionStatuses"
(
	"Id" uuid,
	"GameSessionID" uuid,
	"PlayersCount" int
);