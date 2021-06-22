create table if not exists "Accounts" 
(
	"Id" uuid,
	"RoleId" int,
	"Email" char,
	"FullName" char,
	"PasswordHash" char
);