update application.site_definition_status set site_definition_status_name='Awaiting Review' where site_definition_status_id=3;
insert into application.site_definition_status(site_definition_status_id, site_definition_status_name) values (4, 'Awaiting Spine Update');
insert into application.site_definition_status(site_definition_status_id, site_definition_status_name) values (5, 'Completed');

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;