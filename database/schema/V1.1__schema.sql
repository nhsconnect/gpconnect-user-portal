create table reference.lookup_type
(
    lookup_type_id serial not null,
    lookup_type_name varchar(200) not null,

    constraint reference_lookup_type_lookuptypeid_pk primary key (lookup_type_id),
    constraint reference_lookup_type_lookuptypename_ck check (char_length(trim(lookup_type_name)) > 0)
);

create unique index reference_lookuptype_lookuptypename_ix on reference.lookup_type (lower(lookup_type_name));

create table reference.lookup
(
    lookup_id serial not null,
    lookup_value varchar(500) not null,
    lookup_type_id smallint not null,
	added_date timestamp not null,
	disabled_date timestamp null,	

    constraint reference_lookup_lookupid_pk primary key (lookup_id),
    constraint reference_lookup_lookupvalue_ck check (char_length(trim(lookup_value)) > 0),
    constraint reference_lookup_lookuptypeid_fk foreign key (lookup_type_id) references reference.lookup_type (lookup_type_id)    
);

insert into reference.lookup_type(lookup_type_id, lookup_type_name) values (1, 'CareSetting');
insert into reference.lookup_type(lookup_type_id, lookup_type_name) values (2, 'Supplier');

insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Primary Care',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Community Care',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Social Care',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('A&E',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('NHS 111',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Hospital Access',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Social Care Access',1,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Emergency GP',1,now());

insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('EMIS',2,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('TPP',2,now());
insert into reference.lookup(lookup_value,lookup_type_id,added_date) values ('Vision',2,now());

create table configuration.sso
(
    single_row_lock boolean not null,
    client_id character varying(200) not null,
    client_secret character varying(1000) not null,
    callback_path character varying(1000) not null,
    auth_scheme character varying(100) not null,
    challenge_scheme character varying(100) not null,
    auth_endpoint character varying(1000) not null,
    token_endpoint character varying(1000) not null,
    metadata_endpoint character varying(1000) not null,
    signed_out_callback_path character varying(1000) not null,

    constraint configuration_sso_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_sso_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_sso_clientid_ck check (char_length(trim(client_id)) > 0),
    constraint configuration_sso_clientsecret_ck check (char_length(trim(client_secret)) > 0),
    constraint configuration_sso_callbackpath_ck check (char_length(trim(callback_path)) > 0),
    constraint configuration_sso_authscheme_ck check (char_length(trim(auth_scheme)) > 0),
    constraint configuration_sso_challengescheme_ck check (char_length(trim(challenge_scheme)) > 0),
    constraint configuration_sso_authendpoint_ck check (char_length(trim(auth_endpoint)) > 0),
    constraint configuration_sso_tokenendpoint_ck check (char_length(trim(token_endpoint)) > 0),
    constraint configuration_sso_metadataendpoint_ck check (char_length(trim(metadata_endpoint)) > 0),
    constraint configuration_sso_signedoutcallbackpath_ck check (char_length(trim(signed_out_callback_path)) > 0)
);

grant usage on schema reference to app_user;
grant select, insert, update on all tables in schema reference to app_user;
grant select, update on all sequences in schema reference to app_user;
grant execute on all functions in schema reference to app_user;

grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;