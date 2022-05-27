--
-- Name: logon_user(character varying); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.logon_user(
  _email_address character varying
) RETURNS TABLE(
  user_id integer,
  user_session_id integer,
  email_address character varying,
  is_admin boolean,
  added_date timestamp without time zone,
  last_logon_date timestamp without time zone
)
    LANGUAGE plpgsql
    AS $$
declare
	_user_id integer;
	_user_session_id integer;
	_logon_date timestamp;
	_is_admin boolean;
begin
	_email_address = trim(coalesce(_email_address, ''));
	_logon_date = now();

	select
		u.user_id,
		u.is_admin
	into
		_user_id,
		_is_admin
	from
		application.user u
	where
		lower(u.email_address) = lower(_email_address);

	if (_user_id is null)
	then
		insert into application.user
		(
			email_address,
			added_date,
			authorised_date,
			last_logon_date,
			is_admin
		)
		values
		(
			_email_address,
			_logon_date,
			null,
			null,
			false
		)
		returning
			application.user.user_id,
			application.user.is_admin
		into
			_user_id,
			_is_admin;
	end if;

	if (_is_admin = true)
	then
		update
			application.user u
		set
			last_logon_date = _logon_date
		where
			u.user_id = _user_id;

		insert into application.user_session
		(
			user_id,
			start_time
		)
		values
		(
			_user_id,
			_logon_date
		)
		returning
			user_session.user_session_id
		into
			_user_session_id;
	else
	end if;

	return query
	select
		u.user_id,
		_user_session_id as user_session_id,
		u.email_address,
		_is_admin as is_admin,
		u.added_date,
		u.last_logon_date
	from
		application.user u
	where
		u.user_id = _user_id;
end;
$$;


ALTER FUNCTION application.logon_user(
  _email_address character varying
) OWNER TO postgres;

--
-- Name: FUNCTION logon_user(_email_address character varying); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.logon_user(
  _email_address character varying
) TO app_user;


