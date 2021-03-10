-- Inserindo Vacinas
INSERT INTO public."Vaccines"("Id", "Name", "RequiredDoses", "ExpirationTimeInMonths", "ImmunizationTimeInDays", "ManufacturerId", "CreateDate", "UpdateDate", "MaxTimeNextDose", "MinTimeNextDose")
	VALUES (uuid_generate_v4(),'Coronavac', '2', '6', 14, (select m."Id" from "VaccineManufacturers" m where m."Name" = 'Sinovac/Butantan'),timezone('utc', now()), timezone('utc', now()), 28, 14);
		
INSERT INTO public."Vaccines"("Id", "Name", "RequiredDoses", "ExpirationTimeInMonths", "ImmunizationTimeInDays", "ManufacturerId", "CreateDate", "UpdateDate","MaxTimeNextDose", "MinTimeNextDose")
	VALUES (uuid_generate_v4(),'Covishield (ChAdOx1 nCoV-19/ AZD1222)', '2', '6', 14, (select m."Id" from "VaccineManufacturers" m where m."Name" = 'Oxford/AstraZeneca/Fiocruz'),timezone('utc', now()),timezone('utc', now()),'90', '30');
	
INSERT INTO public."Vaccines"("Id", "Name", "RequiredDoses", "ExpirationTimeInMonths", "ImmunizationTimeInDays", "ManufacturerId", "CreateDate", "UpdateDate","MaxTimeNextDose", "MinTimeNextDose")
	VALUES (uuid_generate_v4(),'Cominarty (Tozinameran/ BNT162b2)', '2', '6', 14, (select m."Id" from "VaccineManufacturers" m where m."Name" = 'Pfizer/BioNTech'),timezone('utc', now()), timezone('utc', now()), '90', '21');
	
-- select* from "Vaccines"