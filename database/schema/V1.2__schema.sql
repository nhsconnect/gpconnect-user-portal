create schema application;

create table application.site_definition
(
    site_definition_id serial not null,
    site_ods_code varchar(50),
    site_party_key varchar(50),
    site_asid varchar(50),
    site_unique_identifier uuid not null,
	
    constraint application_sitedefinition_sitedefinitionid_pk primary key (site_definition_id),
    constraint application_sitedefinition_sitepartykey_ck check (char_length(trim(site_party_key)) > 0),
    constraint application_sitedefinition_siteasid_ck check (char_length(trim(site_asid)) > 0)
);

create unique index application_sitedefinition_siteuniqueidentifier_ix on application.site_definition (site_unique_identifier);

create table application.site_attribute
(
    site_attribute_id serial not null,
    site_definition_id integer not null,
    site_attribute_name varchar(100) not null,
    site_attribute_value varchar(500) not null,	
    constraint application_siteattribute_siteattributeid_pk primary key (site_attribute_id),
    constraint application_siteattribute_siteattributename_ck check (char_length(trim(site_attribute_name)) > 0),
    constraint application_siteattribute_siteattributevalue_ck check (char_length(trim(site_attribute_value)) > 0),	
    constraint application_siteattribute_sitedefinitionid_fk foreign key (site_definition_id) references application.site_definition (site_definition_id) 
);

create unique index application_siteattribute_siteattributename_ix on application.site_attribute (site_definition_id, site_attribute_name);

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;