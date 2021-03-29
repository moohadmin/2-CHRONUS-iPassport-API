-- Inserindo Grupos Prioritários

INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Caminhoneiros', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Comorbidades', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Convênio Corporate', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Forças de segurança e salvamento', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Forças Armadas', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Funcionários do sistema de privação de liberdade', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas de 60 a 64 anos', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas com 60 anos ou mais institucionalizadas', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas de 65 a 69 anos', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas de 70 a 74 anos', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas de 75 a 79 anos', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas de 80 anos ou mais', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas com deficiência permanente grave', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas com deficiência institucionalizadas', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Pessoas em situação de rua', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'População privada de liberdade', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Povos e comunidades tradicionais ribeirinhas', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Povos e comunidades tradicionais quilombolas', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Povos indígenas vivendo em terras indígenas', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores da educação do Ensino Básico (creche, pré-escolas, ensino fundamental, ensino médio, profissionalizantes e EJA)', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores da educação do Ensino Superior', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores de saúde', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores de transporte aéreo', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores de transporte aquaviário', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores de transporte coletivo rodoviário de passageiros', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores de transporte metroviário e ferroviário', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores industriais', timezone('utc', now()), timezone('utc', now()));
	
INSERT INTO public."PriorityGroups"("Id", "Name", "CreateDate", "UpdateDate")
	VALUES (uuid_generate_v4(), 'Trabalhadores portuários', timezone('utc', now()), timezone('utc', now()));
	
-- selecT * from "PriorityGroups"