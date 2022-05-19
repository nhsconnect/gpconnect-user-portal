create table configuration.email
(
    single_row_lock boolean not null,
    sender_address character varying(100) not null,
    host_name character varying(100) not null,
    port smallint not null,
    encryption character varying(10) not null,
    user_name character varying(100) not null,
    password character varying(100) not null,
    default_subject character varying(100) not null,
    constraint configuration_email_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_email_defaultsubject_ck check (char_length(trim(default_subject)) > 0),
    constraint configuration_email_encryption_ck check (char_length(trim(encryption)) > 0),
    constraint configuration_email_hostname_ck check (char_length(trim(host_name)) > 0),
    constraint configuration_email_password_ck check (char_length(trim(password)) > 0),
    constraint configuration_email_port_ck check (port > 0),
    constraint configuration_email_senderaddress_ck check (char_length(trim(sender_address)) > 0),
    constraint configuration_email_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_email_username_ck check (char_length(trim(user_name)) > 0)
);

insert into configuration.email(single_row_lock, sender_address, host_name, port, encryption, user_name, password, default_subject)
values ('true','sender@nhs.net','email.test.com','999','Tls12','sender@nhs.net','abcdef', 'GP Connect End User Portal - Update');

create table configuration.logging
(
    single_row_lock boolean not null,
    token uuid not null,
    channel uuid not null,
    index character varying(100) not null,
    server_url character varying(500) not null,
    source character varying(100) not null,
    source_type character varying(100) not null,
    use_proxy boolean not null,
    proxy_user character varying(100),
    proxy_password character varying(100),

    constraint configuration_logging_singlerowlock_pk primary key (single_row_lock),
    constraint configuration_logging_index_ck check (char_length(trim(index)) > 0),
    constraint configuration_logging_serverurl_ck check (char_length(trim(server_url)) > 0),
    constraint configuration_logging_source_ck check (char_length(trim(source)) > 0),
    constraint configuration_logging_sourcetype_ck check (char_length(trim(source_type)) > 0),
    constraint configuration_logging_singlerowlock_ck check (single_row_lock = true),
    constraint configuration_logging_proxyuser_ck check (char_length(trim(proxy_user)) > 0),
    constraint configuration_logging_proxypassword_ck check (char_length(trim(proxy_password)) > 0)
);

insert into configuration.logging(single_row_lock, token, channel, index, server_url, source, source_type, use_proxy, proxy_user, proxy_password)
values ('true','D5442FDE-6707-4A53-B919-98FC4D6DAC72','0E201D4D-3F48-4D5C-A4DE-EEF8B08A198E','index','http://splunk-server:8088','${logger}','_json', 'true', '***proxy_user***', '***proxy_password***');

grant usage on schema configuration to app_user;
grant select, insert, update on all tables in schema configuration to app_user;
grant select, update on all sequences in schema configuration to app_user;
grant execute on all functions in schema configuration to app_user;