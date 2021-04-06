--Atualiza WasTestPerformed se tiver teste
update  "UserDetails" ud
set "WasTestPerformed" = true 
where "WasTestPerformed" is null
and exists (select 1 from "UserDiseaseTests" udt where udt."UserId" = ud."Id")
