drop table if exists application.email_template;

create table application.email_template
(
    email_template_id smallint not null,
    subject character varying(100),
    body text,
    constraint application_emailtemplate_emailtemplateid_pk primary key (email_template_id),
    constraint application_emailtemplate_body_ck check (char_length(trim(body)) > 0),
    constraint application_emailtemplate_subject_ck check (char_length(trim(subject)) > 0)
);

drop table if exists application.email_recipient;

create table application.email_recipient
(
    email_template_id smallint not null,
    email_address character varying(500),
    constraint application_emailrecipient_emailaddress_ck check (char_length(trim(email_address)) > 0),
    constraint application_emailtemplate_emailtemplateid_fk foreign key (email_template_id) references application.email_template (email_template_id)
);

insert into application.email_template(email_template_id, subject, body) values (1, 'Register GP Connect End User', '<html><head><body>
<h2>A new GP Connect End User registration has been submitted with the following details:</h2>
<div><site_definition></div>
<div><site_attributes></div>
<p><url></p></body></html>');

create table application.site_definition_status
(
	site_definition_status_id smallint not null,
	site_definition_status_name character varying(100),
	constraint application_sitedefinitionstatus_sitedefinitionstatusid_pk primary key (site_definition_status_id),
	constraint application_sitedefinitionstatus_sitedefinitionstatusname_ck check (char_length(trim(site_definition_status_name)) > 0)
);

insert into application.site_definition_status (site_definition_status_id, site_definition_status_name) values (1,'Draft');
insert into application.site_definition_status (site_definition_status_id, site_definition_status_name) values (2,'Submitted');
insert into application.site_definition_status (site_definition_status_id, site_definition_status_name) values (3,'Processed');

alter table application.site_definition add column site_definition_status_id smallint;
update application.site_definition set site_definition_status_id=2;
alter table application.site_definition alter column site_definition_status_id set not null;

alter table application.site_definition add constraint application_sitedefinition_sitedefinitionstatusid_fk foreign key (site_definition_status_id) references application.site_definition_status (site_definition_status_id);

grant usage on schema application to app_user;
grant select, insert, update on all tables in schema application to app_user;
grant select, update on all sequences in schema application to app_user;
grant execute on all functions in schema application to app_user;

