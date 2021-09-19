--Inserindo Profile Nos Users Administrativo
update "Users" 
set "ProfileId" = (select p."Id" from "Profiles" p where p."Key" = 'admin')
where "UserType" = 0 and "ProfileId" is null;

