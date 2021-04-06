--Inserindo Profiles
INSERT INTO public."Profiles" ("Id", "Name", "Key", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Administrativo', 'admin', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."Profiles" ("Id", "Name", "Key", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Empresarial', 'business', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."Profiles" ("Id", "Name", "Key", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Governamental', 'government', timezone('utc', now()), timezone('utc', now()));

INSERT INTO public."Profiles" ("Id", "Name", "Key", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Unidades de Saúde', 'healthUnit', timezone('utc', now()), timezone('utc', now()));
