-- Inserindo tipos de Unidades de Saúde
INSERT INTO public."HealthUnitTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pública', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."HealthUnitTypes"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Privada', timezone('utc', now()), timezone('utc', now()));
	
-- Select * from "HealthUnitTypes"
	
