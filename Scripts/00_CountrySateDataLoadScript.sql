/***************Adicionar Extensão do uuid-ossp****************************/
--SELECT * FROM pg_available_extensions;
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

/************Country Brasil******************/
insert into "Countries" ("Id","Name","Acronym","CreateDate","UpdateDate","Population" ,"ExternalCode" )
values(uuid_generate_v4(),'Brasil','BR', timezone('utc', now()),timezone('utc', now()),null,'BRA');

/***********Brasil States**********************************/
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Acre', 'AC', 12, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Alagoas', 'AL', 27, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Amapá', 'AP', 16, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Amazonas', 'AM', 13, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Bahia', 'BA', 29, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Ceará', 'CE', 23, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Distrito Federal', 'DF', 53, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Espírito Santo', 'ES', 32, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Goiás', 'GO', 52, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Maranhão', 'MA', 21, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Mato Grosso', 'MT', 51, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Mato Grosso do Sul', 'MS', 50, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Minas Gerais', 'MG', 31, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Pará', 'PA', 15, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Paraíba', 'PB', 25, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Paraná', 'PR', 41, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Pernambuco', 'PE', 26, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Piauí', 'PI', 22, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Rio de Janeiro', 'RJ', 33, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Rio Grande do Norte', 'RN', 24, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Rio Grande do Sul', 'RS', 43, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Rondônia', 'RO', 11, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Roraima', 'RR', 14, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Santa Catarina', 'SC', 42, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'São Paulo', 'SP', 35, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Sergipe', 'SE', 28, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
insert into "States" ("Id","Name","Acronym","IbgeCode","CreateDate","UpdateDate","Population","CountryId") 
 values(uuid_generate_v4(), 'Tocantins', 'TO', 17, timezone('utc', now()), timezone('utc', now()), null, (select c."Id" from "Countries" c where c."ExternalCode" = 'BRA'));
