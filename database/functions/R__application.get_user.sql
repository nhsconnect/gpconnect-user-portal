--
-- Name: get_user(character varying); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_user(
  _email_address character varying
) RETURNS TABLE(
  user_id integer,
  email_address character varying,
  last_logon_date timestamp without time zone,
  is_admin boolean,
  added_date timestamp without time zone,
  authorised_date timestamp without time zone
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		u.user_id,
		u.email_address,
		u.last_logon_date,
		u.is_admin,
		u.added_date,
		u.authorised_date
	from
		application.user u
	where
		u.email_address = _email_address;
end;
$$;


ALTER FUNCTION application.get_user(
  _email_address character varying
) OWNER TO postgres;

--
-- Name: FUNCTION get_user(_email_address character varying); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_user(
  _email_address character varying
) TO app_user;


