alter table reference.supplier_product add column product_use_case varchar(1000);

alter table reference.supplier_product_capability add column provider_assured boolean;
alter table reference.supplier_product_capability add column consumer_assured boolean;
alter table reference.supplier_product_capability add column awaiting_assurance boolean;
alter table reference.supplier_product_capability add column assurance_date timestamp without time zone;
alter table reference.supplier_product_capability add column capability_version varchar(50);

alter table reference.supplier_product drop column consumer_assured;
alter table reference.supplier_product drop column provider_assured;

alter table configuration.logging drop column token;
alter table configuration.logging add column token uuid;

alter table configuration.logging add column logs_index character varying(100);
alter table configuration.logging add column web_index character varying(100);
alter table configuration.logging add column error_index character varying(100);

alter table configuration.logging drop column index;

grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;