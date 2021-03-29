-- Inserindo novos endereços
-- Afogados de Ingazeira
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Dr Roberto Nogueira Lima', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'Centro', '232');


INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Avenida Rio Branco', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'Centro', '296');
			

INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Av Artur Padilha ', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'Centro', '537');	


INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Sete De Setembro S/N', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'São Francisco', null);	
			
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Sônia Ricardo S/N', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'São Sebastião', null);	
			
			
-- Recife
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'R. Francisco Alves', '50030230', (select c."Id" from "Cities" c where c."IbgeCode" = '2611606')
			, timezone('utc', now()), timezone('utc', now()), 'Ilha do Leite', '887');	
			
			
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Ramiz Galvão', null, (select c."Id" from "Cities" c where c."IbgeCode" = '2611606')
			, timezone('utc', now()), timezone('utc', now()), 'Arruda', '379');	
			
			
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Artenis', null, (select c."Id" from "Cities" c where c."IbgeCode" = '2611606')
			, timezone('utc', now()), timezone('utc', now()), 'Santo Amaro', '9');	
			
-- Olinda 
			
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'R. Carmelita Muniz de Araújo', '53130150', (select c."Id" from "Cities" c where c."IbgeCode" = '2609600')
			, timezone('utc', now()), timezone('utc', now()), 'Casa Caiada', '225');	
			
					
--Santos

INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Rua Doutor Assis Correa', '11055310', (select c."Id" from "Cities" c where c."IbgeCode" = '3548500')
			, timezone('utc', now()), timezone('utc', now()), 'Gonzaga', '17');	
			
--São Paulo

INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Avenida Kumaki Aoki', '08090370', (select c."Id" from "Cities" c where c."IbgeCode" = '3550308')
			, timezone('utc', now()), timezone('utc', now()), 'Jardim Helena', '785');	

-- João Pessoa
INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Unidade Móvel', null, (select c."Id" from "Cities" c where c."IbgeCode" = '2609600')
			, timezone('utc', now()), timezone('utc', now()), null, null);	

-- select * from "Adresses"  