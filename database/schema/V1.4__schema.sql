create table reference.supplier_product
(
	supplier_id smallint not null,
	supplier_product_id smallint not null,	
	constraint reference_supplierproduct_supplierid_fk foreign key (supplier_id) references reference.lookup (lookup_id),
	constraint reference_supplierproduct_supplierproductid_fk foreign key (supplier_product_id) references reference.lookup (lookup_id)
);

insert into reference.lookup_type(lookup_type_id, lookup_type_name) values (3, 'SupplierProduct');

delete from reference.supplier_product;

delete from reference.lookup where lookup_type_id=2;
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (23,'Advanced','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (24,'Appt Health (MW)','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (25,'AT Tech','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (26,'Cambridge','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (27,'Care Banking','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (28,'Cleric','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (29,'Devon','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (30,'DXC Lorenzo','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (33,'Eva','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (34,'Everylife Technologies','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (35,'HBS','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (36,'IC24','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (37,'Intersystems','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (38,'Leeds Teaching Hospital','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (39,'Meddbase','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (40,'Medloop','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (41,'Medusa','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (42,'MIS-ES','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (43,'Nervecentre','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (44,'North Lincs & Goole','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (45,'Nourish','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (46,'Person Centred Software (PCS)','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (47,'Plymouth','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (48,'Push Dr','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (49,'Restart','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (50,'Servelec','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (51,'Sussex LHCR','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (52,'Sussex Partnership Foundation Trust (SPFT)','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (54,'Humber (Upstream/YHCR)','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (55,'Yorkshire & Humber','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (56,'Vision','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (53,'The Phoenix Partnership Ltd','2',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (31,'EGTON MEDICAL INFORMATION SYSTEMS LTD (EMIS)','2',now());

delete from reference.lookup where lookup_type_id=3;

insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (62,'Product Name 1','3',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (63,'Product Name 2','3',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (64,'Product Name 3','3',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (65,'Product Name 4','3',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (70,'Product Name 5','3',now());
insert into reference.lookup(lookup_id, lookup_value, lookup_type_id, added_date) values (71,'Product Name 6','3',now());

insert into reference.supplier_product(supplier_id, supplier_product_id) values (31,62);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (53,63);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (30,64);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (31,65);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (35,70);
insert into reference.supplier_product(supplier_id, supplier_product_id) values (25,71);

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