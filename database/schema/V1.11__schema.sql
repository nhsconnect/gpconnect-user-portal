create table reference.supplier_product_capability
(
    supplier_product_capability_id serial not null,
    supplier_product_id smallint not null,
    product_capability_id smallint not null,

    constraint reference_supplierproductcapability_productcapabilityid_pk primary key (supplier_product_capability_id),
    constraint reference_supplierproductcapability_supplierproductid_fk foreign key (supplier_product_id) references reference.lookup (lookup_id),
    constraint reference_supplierproductcapability_productcapabilityid_fk foreign key (product_capability_id) references reference.lookup (lookup_id)
);

insert into reference.lookup_type (lookup_type_id, lookup_type_name, lookup_type_description) values (4, 'Capability', 'Supplier Product Capability');

insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('HTML', 4, now());
insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('Structured', 4, now());
insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('Appointments', 4, now());

grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;