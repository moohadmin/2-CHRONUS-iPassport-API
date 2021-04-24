--ADD COMPANY SEGMENT
--GOVERNMENT
INSERT INTO public."CompanySegments"
("Id", "Name", "Identifyer", "CompanyTypeId", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Municipal', 1, (select "Id" from "CompanyTypes" where "Identifyer" = 1), timezone('utc', now()), timezone('utc', now()));
INSERT INTO public."CompanySegments"
("Id", "Name", "Identifyer", "CompanyTypeId", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Estadual', 2, (select "Id" from "CompanyTypes" where "Identifyer" = 1), timezone('utc', now()), timezone('utc', now()));
INSERT INTO public."CompanySegments"
("Id", "Name", "Identifyer", "CompanyTypeId", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Federal', 3, (select "Id" from "CompanyTypes" where "Identifyer" = 1), timezone('utc', now()), timezone('utc', now()));
--PRIVATE
INSERT INTO public."CompanySegments"
("Id", "Name", "Identifyer", "CompanyTypeId", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Contratante', 4, (select "Id" from "CompanyTypes" where "Identifyer" = 2), timezone('utc', now()), timezone('utc', now()));
INSERT INTO public."CompanySegments"
("Id", "Name", "Identifyer", "CompanyTypeId", "CreateDate", "UpdateDate")
VALUES(uuid_generate_v4(), 'Saúde', 5, (select "Id" from "CompanyTypes" where "Identifyer" = 2), timezone('utc', now()), timezone('utc', now()));