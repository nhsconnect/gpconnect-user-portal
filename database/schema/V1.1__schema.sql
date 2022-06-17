DROP TYPE application.site_attributes_type;

CREATE TYPE application.site_attributes_type AS
(
	sitename character varying,
	selectedccgodscode character varying,
	selectedccgname character varying,
	isappointmentenabled boolean,
	ishtmlenabled boolean,
	isstructuredenabled boolean,
	issenddocumentenabled boolean,
	sitepostcode character varying,
	siteaddressline1 character varying,
	siteaddressline2 character varying,
	siteaddresstown character varying,
	siteaddresscounty character varying,
	siteaddresscountry character varying,
	odscode character varying,
	selectedsupplier character varying,
	usecasedescription character varying
);

ALTER TYPE application.site_attributes_type OWNER TO postgres;
