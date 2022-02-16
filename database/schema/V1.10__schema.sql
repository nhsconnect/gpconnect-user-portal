update application.site_definition set site_definition_status_id=4 where site_definition_status_id=5;

delete from application.site_definition_status where site_definition_status_id=5;

update application.site_definition_status set site_definition_status_name='Awaiting Review' where site_definition_status_id=2;
update application.site_definition_status set site_definition_status_name='Awaiting Spine Update' where site_definition_status_id=3;
update application.site_definition_status set site_definition_status_name='Completed' where site_definition_status_id=4;

alter table reference.lookup_type add column lookup_type_description varchar(200);
update reference.lookup_type set lookup_type_description='Care Setting' where lookup_type_id=1;
update reference.lookup_type set lookup_type_description='Supplier' where lookup_type_id=2;
update reference.lookup_type set lookup_type_description='Supplier Product' where lookup_type_id=3;
alter table reference.lookup_type alter column lookup_type_description set not null;

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;

grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;