--
-- Name: set_user_isadmin(integer, integer, boolean); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.set_user_isadmin(
  _admin_user_id integer,
  _user_id integer,
  _is_admin boolean
) RETURNS void
    LANGUAGE plpgsql
    AS $$
begin
	update
		application.user
	set
		is_admin = _is_admin,
		authorised_date = case when _is_admin = true then now() else null end
	where
		user_id = _user_id;
end;
$$;


ALTER FUNCTION application.set_user_isadmin(
  _admin_user_id integer,
  _user_id integer,
  _is_admin boolean
) OWNER TO postgres;

--
-- Name: FUNCTION set_user_isadmin(_admin_user_id integer, _user_id integer, _is_admin boolean); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.set_user_isadmin(
  _admin_user_id integer,
  _user_id integer,
  _is_admin boolean
) TO app_user;


