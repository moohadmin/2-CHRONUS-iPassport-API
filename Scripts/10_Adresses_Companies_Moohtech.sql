-- Inserindo Endereço da Mooh Tech

INSERT INTO public."Adresses"("Id", "Description", "Cep", "CityId", "CreateDate", "UpdateDate")
    VALUES (uuid_generate_v4(), 'Avenida paulista, 854, Bela Vista', '01310913', (select c."Id" from "Cities" c where c."IbgeCode" = '3550308'), timezone('utc', now()), timezone('utc', now()));

-- Inserindo Empresa Mooh Tech
			
INSERT INTO public."Companies"("Id", "Name", "Cnpj", "AddressId", "CreateDate", "UpdateDate")
    VALUES (uuid_generate_v4(), 'MOOH TECH', '28070032000182', (select a."Id" from "Adresses" a where a."Description" = 'Avenida paulista, 854, Bela Vista'),timezone('utc', now()), timezone('utc', now()));

