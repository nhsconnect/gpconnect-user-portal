drop function if exists application.get_email_template;

create function application.get_email_template
(
	_mail_template_id integer
)
returns table
(
	email_template_id smallint,
	subject varchar(100),
	body text,
	recipients text
)
as $$
begin
	return query
	select
		et.email_template_id,
		et.subject,
		et.body,
		(select string_agg(er.email_address, ', ') from application.email_recipient er where er.email_template_id = _mail_template_id) recipients
	from 
		application.email_template et
	where
		et.email_template_id = _mail_template_id;
end;
$$ language plpgsql;