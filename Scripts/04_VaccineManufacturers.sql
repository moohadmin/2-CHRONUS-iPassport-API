-- Inserindo Fabricantes
INSERT INTO public."VaccineManufacturers"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Sinovac/Butantan', timezone('utc', now()), timezone('utc', now()));
		
INSERT INTO public."VaccineManufacturers"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pfizer/BioNTech', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."VaccineManufacturers"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Oxford/AstraZeneca/Fiocruz', timezone('utc', now()), timezone('utc', now()));
	
-- select * from VaccineManufacturers

