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
			(select "Id" from "Adresses" where "Description" = 'Rua Dr Roberto Nogueira Lima'), timezone('utc', now()),
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
			(select "Id" from "Adresses" where "Description" = 'Avenida Rio Branco'), timezone('utc', now()),
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
			(select "Id" from "Adresses" where "Description" = 'Rua Sonia Ricardo S/N'), timezone('utc', now()),
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