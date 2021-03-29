-- Inserindo Tipos Sanguíneos
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'A+', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'A-', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'AB+', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'AB-', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'B+', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'B-', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'O+', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."BloodTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'O-', timezone('utc', now()), timezone('utc', now()));
 
-- selecT * from "BloodTypes"