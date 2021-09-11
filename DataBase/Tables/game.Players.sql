create table if not exists "game"."Players"
(
    "Id"       uuid primary key,
    "Nickname" varchar(50) not null
);
