create table reference.supplier_product
(
	supplier_id smallint not null,
	supplier_product_id smallint not null,	
	constraint reference_supplierproduct_supplierid_fk foreign key (supplier_id) references reference.lookup (lookup_id),
	constraint reference_supplierproduct_supplierproductid_fk foreign key (supplier_product_id) references reference.lookup (lookup_id)
);

insert into reference.lookup_type(lookup_type_id, lookup_type_name) values (3, 'SupplierProduct');

insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('HTML', '3', now());
insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('Appointment Management', '3', now());
insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('Structured Meds and Allergies', '3', now());

insert into reference.supplier_product(supplier_id, supplier_product_id) values (23, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (23, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (24, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (25, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (26, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (27, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (28, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (28, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (29, 59);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (30, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (30, 59);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (31, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (31, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (32, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (33, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (34, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (35, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (36, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (36, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (37, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (37, 59);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (38, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (39, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (40, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (41, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (42, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (42, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (43, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (44, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (45, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (46, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (47, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (48, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (49, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (50, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (51, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (52, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (53, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (53, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (54, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (55, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (56, 57);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (56, 58);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (56, 59);

alter table application.site_definition drop constraint application_sitedefinition_sitepartykey_ck;
alter table application.site_definition drop constraint application_sitedefinition_siteasid_ck;
alter table application.site_attribute drop constraint application_siteattribute_siteattributevalue_ck;

create extension if not exists "uuid-ossp";

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;

grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;