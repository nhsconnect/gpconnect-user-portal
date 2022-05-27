--
-- Name: get_users(); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_users() RETURNS TABLE(
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
	order by
		u.email_address;
end;
$$;


ALTER FUNCTION application.get_users() OWNER TO postgres;

--
-- Name: FUNCTION get_users(); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_users() TO app_user;


