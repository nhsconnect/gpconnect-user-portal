/*
    Schema V1.0: Initial schema
*/


/* 
    create schemas
*/
create schema reference;
create schema configuration;

/*
    create tables - reference
*/

create table reference.organisation
(
    organisation_id serial not null,
    organisation_name varchar(100) not null,
    ods_code varchar(10) not null,
    org_status varchar(50) not null,
    org_record_class varchar(10) not null,
    postcode varchar(10) not null,
    last_change_date timestamp not null,
    primary_role_id varchar(10) not null,
    primary_role_description varchar(100) not null,
    organisation_link varchar(1000) not null,
    added_date timestamp not null,
    last_sync_date timestamp not null,

    constraint reference_organisation_organisationid_pk primary key (organisation_id),
    constraint reference_organisation_odscode_uq unique (ods_code), 
    constraint reference_organisation_odscode_ck_1 check (char_length(trim(ods_code)) > 0),
    constraint reference_organisation_odscode_ck_2 check (upper(ods_code) = ods_code),
    constraint reference_organisation_orgstatus_ck check (char_length(trim(org_status)) > 0),
    constraint reference_organisation_orgrecordclass_ck check (char_length(trim(org_record_class)) > 0),
    constraint reference_organisation_postcode_ck check (char_length(trim(postcode)) > 0),
    constraint reference_organisation_primaryroleid_ck check (char_length(trim(primary_role_id)) > 0),
    constraint reference_organisation_primary_roledescription_ck check (char_length(trim(primary_role_description)) > 0),
    constraint reference_organisation_organisationlink_ck check (char_length(trim(organisation_link)) > 0),
    constraint reference_organisation_organisationname_ck check (char_length(trim(organisation_name)) > 0)
);

create table configuration.general
(
    single_row_lock boolean,
    product_name varchar(100),
    product_version varchar(100),

    constraint configuration_general_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_general_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_general_productname_ck check (char_length(trim(product_name)) > 0),
    constraint configuration_general_productversion_ck check (char_length(trim(product_version)) > 0)
);

insert into configuration.general
(
	single_row_lock,
	product_name,
	product_version
)
values
(
	true,
	'GP Connect End User Portal',
	'1.0.0.0'
);

create table configuration.reference
(
    single_row_lock boolean,
    host_name varchar(100),

    constraint configuration_reference_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_reference_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_reference_hostname_ck check (char_length(trim(host_name)) > 0)
);

insert into configuration.reference
(
    single_row_lock,
    host_name
)
values
(
    true,
    'directory.spineservices.nhs.uk'
);

create table configuration.reference_api_query
(
    query_name varchar(100),
    query_text varchar(1000),

    constraint configuration_referenceapiquery_queryname_pk primary key (query_name)
);

insert into configuration.reference_api_query
(
    query_name,
    query_text
)
values
(
    'GetCCGOrganisationsFromSDS',
    '/ord/2-0-0/organisations?roles=RO98&limit=1000'
);

-- create user
create user app_user;
alter user app_user valid until 'infinity';

-- revoke default public permissions
revoke all on schema public from public;

-- grant schema application usage
grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;

-- grant schema configuration usage
grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;