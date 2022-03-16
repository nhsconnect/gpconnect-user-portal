alter table application.site_definition add column added_date timestamp;
alter table application.site_definition add column last_updated timestamp;
update application.site_definition set added_date = now();
alter table application.site_definition alter column added_date set not null;

alter table application.site_attribute add column added_date timestamp not null;
alter table application.site_attribute add column last_updated timestamp;
