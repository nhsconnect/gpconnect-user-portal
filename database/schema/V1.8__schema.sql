alter table application.site_attribute alter column site_attribute_value drop not null;
alter table application.site_attribute drop constraint application_siteattribute_siteattributename_ck;

alter table application.site_definition add column site_interactions varchar(4000) null;

alter table configuration.general add column admin_product_name varchar(100) null;
update configuration.general set admin_product_name = 'GP Connect Enablement Team Portal';
alter table configuration.general alter column admin_product_name set not null;

alter table configuration.general add column get_access_email_address varchar(100) null;
update configuration.general set get_access_email_address = 'test@nhs.net';
alter table configuration.general alter get_access_email_address set not null;

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;

grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;