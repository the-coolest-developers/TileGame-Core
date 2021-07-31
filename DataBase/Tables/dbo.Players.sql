create table if not exists "Players" 
(
	"Id" uuid,
	"GameSessionId" uuid, 
	foreign key ("GameSessionId") references "GameSessions"("Id") 	
);