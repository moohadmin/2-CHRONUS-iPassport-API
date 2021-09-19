--Atualizar a empresa da unidade de saúde
--Para cada Unidade
update "HealthUnits" 
set "CompanyId" = (select "Id" from "Companies" where "Name" = 'exemplo') --Colocar o Id ou pesquisar por algum campo name/cnpj/ine/
where "CompanyId" is null 
and "Name"  = 'exemplo' --Mudar para o nome da healthUnit ou Id
--"Id"  = 'id'