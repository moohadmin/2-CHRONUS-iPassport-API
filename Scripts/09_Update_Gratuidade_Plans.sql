-- Alterar período de gratuidade para 30/04/2021 e Active do Premium para false.

UPDATE public."Plans"
	SET  "Observation"= 'Gratuito até 30/04/2021.',
	     "Active" = 'false'
	WHERE "Type" in ('Premium','Corporativo');
	
--select * from "Plans"