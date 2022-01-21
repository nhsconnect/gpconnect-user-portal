create table configuration.spine
(
    single_row_lock boolean not null,
    spine_fhir_api_directory_services_fqdn character varying(100) not null,
    spine_fhir_api_systems_register_fqdn character varying(100) not null,
    spine_fhir_api_key character varying(100) not null,

    constraint configuration_spine_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_spine_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_spine_spinefhirapidirectoryservicesfqdn_ck check (char_length(trim(spine_fhir_api_directory_services_fqdn)) > 0),
    constraint configuration_spine_spinefhirapisystemsregisterfqdn_ck check (char_length(trim(spine_fhir_api_systems_register_fqdn)) > 0),
    constraint configuration_spine_spinefhirapikey_ck check (char_length(trim(spine_fhir_api_key)) > 0)
);

insert into configuration.spine(single_row_lock, spine_fhir_api_directory_services_fqdn, spine_fhir_api_systems_register_fqdn, spine_fhir_api_key)
values (true, 'https://test.com', 'https://test.com', 'ABC123');

create table configuration.fhir_api_query
(
    query_name character varying(100) not null,
    query_text character varying(1000) not null,
    constraint configuration_sdsquery_queryname_pk primary key (query_name),
    constraint configuration_sdsquery_queryname_ck check (char_length(trim(query_name)) > 0),
    constraint configuration_sdsquery_querytext_ck check (char_length(trim(query_text)) > 0)
);

alter table reference.supplier_product add column consumer_assured bool;
alter table reference.supplier_product add column provider_assured bool;
update application.site_definition set site_definition_status_id=2;
alter table application.site_definition alter column site_definition_status_id set not null;

alter table application.site_definition add constraint application_sitedefinition_sitedefinitionstatusid_fk foreign key (site_definition_status_id) references application.site_definition_status (site_definition_status_id);

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;

grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;

