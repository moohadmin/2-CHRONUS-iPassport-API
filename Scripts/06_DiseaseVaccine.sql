INSERT INTO public."DiseaseVaccine"("DiseasesId", "VaccinesId")
	VALUES ((select d."Id" from "Diseases" d where d."Name" = 'Covid-19'), (select v."Id" from "Vaccines" v where v."Name" = 'Coronavac'));
	
INSERT INTO public."DiseaseVaccine"("DiseasesId", "VaccinesId")
	VALUES ((select d."Id" from "Diseases" d where d."Name" = 'Covid-19'), (select v."Id" from "Vaccines" v where v."Name" = 'Covishield (ChAdOx1 nCoV-19/ AZD1222)'));
	
INSERT INTO public."DiseaseVaccine"("DiseasesId", "VaccinesId")
	VALUES ((select d."Id" from "Diseases" d where d."Name" = 'Covid-19'), (select v."Id" from "Vaccines" v where v."Name" = 'Cominarty (Tozinameran/ BNT162b2)'));
	
--select * from "DiseaseVaccine"