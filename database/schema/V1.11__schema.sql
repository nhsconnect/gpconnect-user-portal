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
insert into reference.lookup(lookup_value, lookup_type_id, added_date) values ('SendDocument', 4, now());

create table application.user
(
    user_id serial not null,
    email_address varchar(200) not null,
    last_logon_date timestamp without time zone,
    is_admin boolean not null default false,
    added_date timestamp without time zone not null,
    authorised_date timestamp without time zone,

    constraint application_user_userid_pk primary key (user_id),
    constraint application_user_emailaddress_ck check (char_length(trim(email_address)) > 0)
);

create unique index application_user_emailaddress_ix on application.user (lower(email_address));

UPDATE application.email_template
	SET body='"<html><head><body>
<h2>A new <application_name> registration has been submitted with the following details:</h2>
<h3>Submitted at <generated_date_time></h3>
<div style="margin-bottom:10px"><site_definition></div>
<div style="margin-bottom:10px"><site_attributes></div>
<p><url></p></body></html>"'
	WHERE email_template_id=1;

create table application.user_session
(
    user_session_id serial not null,
    user_id integer not null,
    start_time timestamp without time zone,
    end_time timestamp without time zone
);

grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;