create table if not exists "Accounts" 
(
	"Id" uuid,
	"RoleId" smallint,
	"Email" varchar(50),
	"FullName" varchar(50),
	"PasswordHash" varchar(50)
);