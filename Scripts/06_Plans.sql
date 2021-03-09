-- Inserindo Planos
INSERT INTO public."Plans"("Id", "Type", "Description", "Price", "Observation", "CreateDate", "UpdateDate", "Active", "ColorEnd", "ColorStart")
	VALUES (uuid_generate_v4(), 'Gratuito','Agende a sua vacinação na Rede Pública de Saúde.', '0', '', timezone('utc', now()), timezone('utc', now()), 'true', '4CB8ED', '0075BE');
	
INSERT INTO public."Plans"("Id", "Type", "Description", "Price", "Observation", "CreateDate", "UpdateDate", "Active", "ColorEnd", "ColorStart")
	VALUES (uuid_generate_v4(), 'Premium','Agende a sua vacinação na Rede Pública de Saúde e/ou na Rede Privada de Atendimento.', '0', 'Gratuito até 30/03/2021.', timezone('utc', now()), timezone('utc', now()), 'true', 'CCAD57', 'A27820');
			
INSERT INTO public."Plans"("Id", "Type", "Description", "Price", "Observation", "CreateDate", "UpdateDate", "Active", "ColorEnd", "ColorStart")
	VALUES (uuid_generate_v4(), 'Corporativo','Agende a sua vacinação na Rede Pública de Saúde e/ou na Rede Privada de Atendimento - Plano Exclusivo para funcionários de Empresas vinculadas.','0', 'Gratuito até 30/03/2021.', timezone('utc', now()), timezone('utc', now()), 'false', 'C7C8C9', '6C6C6C');
			
-- select * from "Plans"



