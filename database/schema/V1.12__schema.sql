drop type application.site_attributes_type;

create type application.site_attributes_type as (SiteName varchar, SelectedCCGOdsCode varchar, SelectedCCGName varchar, IsAppointmentEnabled boolean, IsHtmlEnabled boolean, IsStructuredEnabled boolean, IsSendDocumentEnabled boolean, SitePostcode varchar, OdsCode varchar, SelectedSupplier varchar, UseCaseDescription varchar);

alter table reference.lookup_type add is_system boolean default false;

update reference.lookup_type set is_system='true' where lookup_type_name in ('CCGICBODSCode','CCGICBName','SupplierProduct','Capability');

INSERT INTO application.site_definition_status(site_definition_status_id, site_definition_status_name) VALUES (5, 'Live');

alter table application.site_definition add column master_site_unique_identifier uuid default null;

alter table application.site_definition add c
grant usage on type application.site_attributes_type to app_user;

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;