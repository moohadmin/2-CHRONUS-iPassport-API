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
	VALUES (uuid_generate_v4(), 'Rua Dr Roberto Nogueira Lima, 232', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'Centro', '232');


INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Avenida Rio Branco, 296', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
			, timezone('utc', now()), timezone('utc', now()), 'Centro', '296');
			

INSERT INTO public."Adresses"(
	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate", "District", "Number")
	VALUES (uuid_generate_v4(), 'Av Artur Padilha', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104')
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

--*******************************************************************************************

-- Inserindo Unidades de Saúde
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'UBS Mandacaru I', '10346096000106', null, null, null, null, null, (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Dr Roberto Nogueira Lima'), timezone('utc', now()), timezone('utc', now()),
			'true', '7280602'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'UBS Mandacaru II', '10346096000106', null, null, null, null, null, (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Dr Roberto Nogueira Lima, 232'), timezone('utc', now()),
			timezone('utc', now()), 'true', '7320647'); 
			
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'Vigilância em Saúde', null, null, null, null, null, null, (select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Avenida Rio Branco'), timezone('utc', now()),
			timezone('utc', now()), 'true', '2429411'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'Central de Regulação Regional de Afogados da Ingazeira', '10346096000106', null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Avenida Rio Branco, 296'), timezone('utc', now()),
			timezone('utc', now()), 'true', '7129386'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'X Gerência Regional De Saúde Afogados Da Ingazeira', '10572048000128', null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Av Artur Padilha'), timezone('utc', now()),
			timezone('utc', now()), 'true', '5700523'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'PSF São Francisco', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Sete De Setembro S/N'), timezone('utc', now()),
			timezone('utc', now()), 'true', '5874262'); 
			
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'PSF São Sebastião', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Sônia Ricardo S/N'), timezone('utc', now()),
			timezone('utc', now()), 'true', '2429500'); 	
		
			
				
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'HOPE', '09464629000167', null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Privada'), 
			(select "Id" from "Adresses" where "Description" = 'R. Francisco Alves'), timezone('utc', now()),
			timezone('utc', now()), 'true', '2355922'); 
			
		
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'Drive Thru Arruda', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Ramiz Galvão'), timezone('utc', now()),
			timezone('utc', now()), 'true', '3302032'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'Drive Thru Justiça do Trabalho', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Artenis'), timezone('utc', now()),
			timezone('utc', now()), 'true', '0022187'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'Unidade de Saúde Familiar Móvel', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Unidade Móvel' and "CityId" = 'ac5f28a1-b88f-4d8c-8d24-1a6d9c396095'),
			timezone('utc', now()),	timezone('utc', now()), 'true', '7419473'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'SEC Saúde Olinda', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'R. Carmelita Muniz de Araújo'), timezone('utc', now()),
			timezone('utc', now()), 'true', '6539017'); 
			
INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'SEUB Gonzaga', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Rua Doutor Assis Correa'), timezone('utc', now()),
			timezone('utc', now()), 'true', '2054205'); 

INSERT INTO public."HealthUnits"(
	"Id", "Name", "Cnpj", "Email", "ResponsiblePersonName", "ResponsiblePersonPhone", "ResponsiblePersonOccupation", "DeactivationDate",
	"TypeId", "AddressId", "CreateDate", "UpdateDate", "Active", "Ine")
	VALUES (uuid_generate_v4(), 'UBS Jardim Helena', null, null, null, null, null, null, 
			(select "Id" from "HealthUnitTypes" where "Name" = 'Pública'), 
			(select "Id" from "Adresses" where "Description" = 'Avenida Kumaki Aoki'), timezone('utc', now()),
			timezone('utc', now()), 'true', '4049934'); 
			
-- selecT * from "HealthUnits" 