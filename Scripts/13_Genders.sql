-- Inserindo Gêneros
INSERT INTO public."Genders"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Masculino', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."Genders"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Feminino', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."Genders"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Não Binário', timezone('utc', now()), timezone('utc', now()));
	
-- selecT * from "Genders"
	

