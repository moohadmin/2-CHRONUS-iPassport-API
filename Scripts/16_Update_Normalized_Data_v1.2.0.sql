#UserDetails
#De PriorityGroup Para PriorityGroupId, Fk para PriorityGroups.Id

select count(*), "PriorityGroup" from "UserDetails" group by "PriorityGroup";

select * from "UserDetails" ud
inner join "PriorityGroups" pg on ud."PriorityGroup" = pg."Name";

update "UserDetails" ud set "PriorityGroupId" =
(select pg."Id" from "PriorityGroups" pg where pg."Name" = ud."PriorityGroup");

#Users
#De BloodType para BloodTypeId, Fk para BloodTypes.Id
select count(*), "BloodType" from "Users" group by "BloodType";

select u."BloodType", u."BloodTypeId", bd."Id", bd."Name" from "Users" u
inner join "BloodTypes" bd on u."BloodType" = bd."Name";

update "Users" u set "BloodTypeId" =
(select bd."Id" from "BloodTypes" bd where bd."Name" = u."BloodType");

#De Gender para GenderId, Fk para Genders.Id
select count(*), "Gender" from "Users" group by "Gender";

update "Users" set "Gender" = 'Masculino' where "Gender" = 'Male';
update "Users" set "Gender" = 'Feminino' where "Gender" = 'Female';

select u."Gender", u."GenderId", g."Id", g."Name" from "Users" u
inner join "Genders" g on u."Gender" = g."Name";

update "Users" u set "GenderId" =
(select g."Id" from "Genders" g where g."Name" = u."Gender");

#De Breed para HumanRaceId, Fk para HumanRaces.Id
select count(*), "Breed" from "Users" group by "Breed";

update "Users" set "Breed" = 'Branco' where "Breed" = 'White';
update "Users" set "Breed" = 'Preto' where "Breed" = 'Black';

select u."Breed", u."HumanRaceId", hr."Id", hr."Name" from "Users" u
inner join "HumanRaces" hr on u."Breed" = hr."Name";

update "Users" u set "HumanRaceId" =
(select hr."Id" from "HumanRaces" hr where hr."Name" = u."Breed");

#Adresses (Ganhou as colunas District,Number antes estava tudo junto com Description)
#De Description Para District
#De Description Para Number
select * from "Adresses" where "Description" is not null;

update "Adresses" set "Number" = '871', "District" = 'Bairro da Boa Vista', "Description" = 'Rua Dom Bosco' where "Description" = 'Rua Dom Bosco n 871, Bairro da Boa Vista';
update "Adresses" set "Number" = '296', "District" = 'Centro', "Description" = 'Avenida Rio Branco' where "Description" = 'Avenida Rio Branco, 296 - Centro';
update "Adresses" set "Number" = '245', "District" = 'Bela Vista', "Description" = 'Rua São Vicente 237' where "Description" = 'Rua São Vicente 237, 245 – BELA VISTA';
update "Adresses" set "Number" = '854', "District" = 'Bela Vista', "Description" = 'Avenida Paulista' where "Description" = 'Avenida paulista, 854, Bela Vista';
update "Adresses" set "Number" = '49', "District" = 'Vila Belmiro', "Description" = 'Dom Pedro I' where "Description" = 'Dom Pedro I, 49, Vila Belmiro';
update "Adresses" set "Number" = '741', "District" = null, "Description" = 'Avenida Beira Rio' where "Description" = 'Avenida Beira Rio, 741';
update "Adresses" set "Number" = '925', "District" = 'Bairro do Recife', "Description" = 'Av. Cais do Apolo' where "Description" = 'Av. Cais do Apolo, 925, Bairro do Recife';

#UserVaccines (eram 3 colunas na tabela e hoje é tabela healthunit com suas Fk's)
#De UnitName para HealthUnitId, FK HealthUnits.Id -> HealthUnits.Name
#De CityId para HealthUnitId, Fk HealthUnits.Id -> Fk HealthUnits.AddressId -> Adresses.CityId
#De UnityType para HealthUnitId, FK HealthUnits.Id -> FK HealthUnits.TypeId -> HealthUnitTypes.Id

select count(*), "UnitName", "CityId", "UnityType" from "UserVaccines" group by "UnitName", "CityId", "UnityType";

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), '2f18853f-1768-49b3-a4cb-b52b551d970c', 'Endereço Unidade 1 Tipo Pública', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 1', (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 1 Tipo Pública'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 1') where "CityId" = '2f18853f-1768-49b3-a4cb-b52b551d970c' and "UnityType" = '1';

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), '6781964b-c7a8-4524-b871-257bf77962db', 'Endereço Unidade 2 Tipo Pública', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 2', (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 2 Tipo Pública'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 2') where "CityId" = '6781964b-c7a8-4524-b871-257bf77962db' and "UnityType" = '1';

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), '05d9c27e-4cc8-4bfd-8e95-d4b56fb8233c', 'Endereço Unidade 3 Tipo Pública', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 3', (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 3 Tipo Pública'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 3') where "CityId" = '05d9c27e-4cc8-4bfd-8e95-d4b56fb8233c' and "UnityType" = '1';

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), '97d5e825-645f-4451-8066-795183138215', 'Endereço Unidade 4 Tipo Pública', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 4', (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 4 Tipo Pública'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 4') where "CityId" = '97d5e825-645f-4451-8066-795183138215' and "UnityType" = '1';

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'a37c98c8-4dad-4fee-8295-bf600e64a0c2', 'Endereço Unidade 5 Tipo Pública', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 5', (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 5 Tipo Pública'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 5') where "CityId" = 'a37c98c8-4dad-4fee-8295-bf600e64a0c2' and "UnityType" = '1';

INSERT INTO "Adresses" ("Id", "CityId", "Description", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), '6781964b-c7a8-4524-b871-257bf77962db', 'Endereço Unidade 6 Tipo Privada', timezone('utc', now()), timezone('utc', now()));

INSERT INTO "HealthUnits" ("Id", "Name", "TypeId", "AddressId", "CreateDate", "UpdateDate")
VALUES (uuid_generate_v4(), 'Unidade 6', (select "Id" from "HealthUnitTypes" where "Name" = 'Privada'), (select "Id" from "Adresses" where "Description" = 'Endereço Unidade 6 Tipo Privada'), timezone('utc', now()), timezone('utc', now()));

update "UserVaccines" set "HealthUnitId" = (select "Id" from "HealthUnits" where "Name" = 'Unidade 6') where "CityId" = '6781964b-c7a8-4524-b871-257bf77962db' and "UnityType" = '2';

select uv."Id", uv."HealthUnitId", hu."Id", uv."UnitName", hu."Name", uv."UnityType", hu."TypeId", hut."Id", hut."Name", uv."CityId", hu."AddressId", a."Id", a."Description", a."CityId" from "UserVaccines" uv
inner join "HealthUnits" hu on uv."HealthUnitId" = hu."Id"
inner join "HealthUnitTypes" hut on hu."TypeId" = hut."Id"
inner join "Adresses" a on hu."AddressId" = a."Id";