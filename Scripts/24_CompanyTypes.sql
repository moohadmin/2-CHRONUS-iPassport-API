--ADD COMPANY TYPES
INSERT INTO public."CompanyTypes"
("Id", "Name", "Identifyer", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Governamental', 1, timezone('utc', now()), timezone('utc', now()));
INSERT INTO public."CompanyTypes"
("Id", "Name", "Identifyer", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Privada', 2, timezone('utc', now()), timezone('utc', now()));
