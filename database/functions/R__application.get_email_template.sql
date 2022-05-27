--
-- Name: get_email_template(integer); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_email_template(
  _mail_template_id integer
) RETURNS TABLE(
  email_template_id smallint,
  subject character varying,
  body text,
  recipients text
)
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION application.get_email_template(
  _mail_template_id integer
) OWNER TO postgres;

