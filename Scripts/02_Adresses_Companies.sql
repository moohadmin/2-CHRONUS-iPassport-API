-- Inserindo Endereços
INSERT INTO public."Adresses"("Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Rua Dom Bosco n 871, Bairro da Boa Vista', '50070070', (select c."Id" from "Cities" c where c."IbgeCode" = '2611606'), timezone('utc', now()), timezone('utc', now()));


INSERT INTO public."Adresses"(	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Avenida Rio Branco, 296 - Centro', '56800000', (select c."Id" from "Cities" c where c."IbgeCode" = '2600104'), timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."Adresses"(	"Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Rua São Vicente 237, 245 – BELA VISTA', '01314010', (select c."Id" from "Cities" c where c."IbgeCode" = '3550308'),timezone('utc', now()), timezone('utc', now()));

-- Inserindo Empresas
INSERT INTO public."Companies"("Id", "Name", "Cnpj", "AddressId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'FEDERAÇÃO PERNAMBUCANA DE FUTEBOL - FPF/PE', '10956258000110', (select a."Id" from "Adresses" a where a."Description" = 'Rua Dom Bosco n 871, Bairro da Boa Vista' and a."Cep" = '50070070'), timezone('utc', now()), timezone('utc', now()));
			
INSERT INTO public."Companies"(	"Id", "Name", "Cnpj", "AddressId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'FUNDO MUNICIPAL DE SAUDE DE AFOGADOS DA INGAZEIRA-PE', '11308823000103', (select a."Id" from "Adresses" a where a."Description" = 'Avenida Rio Branco, 296 - Centro' and a."Cep" = '56800000'), timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."Companies"("Id", "Name", "Cnpj", "AddressId", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'TECNOLOGIA BANCÁRIA S/A', '51427102000471', (select a."Id" from "Adresses" a where a."Description" = 'Rua São Vicente 237, 245 – BELA VISTA' and a."Cep" = '01314010'),timezone('utc', now()), timezone('utc', now()));
			

