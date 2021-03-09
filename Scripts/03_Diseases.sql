-- Inserindo Doença
INSERT INTO public."Diseases"("Id", "Name", "Description", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Covid-19', 'Covid-19', timezone('utc', now()), timezone('utc', now()));
	
-- select * from "Diseases"