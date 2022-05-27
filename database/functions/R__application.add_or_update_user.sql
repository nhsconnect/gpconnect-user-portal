--
-- Name: add_or_update_user(character varying, boolean); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.add_or_update_user(
	_email_address character varying,
	_is_admin boolean
) RETURNS TABLE(
	user_id integer,
	email_address character varying,
	site_asid character varying,
	site_unique_identifier uuid,
	site_definition_id integer,
	site_definition_status_id smallint,
	site_interactions character varying
)
    LANGUAGE plpgsql
    AS $$
declare	_site_definition_id integer;
begin
	if not exists
	(
		select
			*
		from
			application.user u
		where
			u.user_id = _user_id
	)
	then
		insert into application.site_definition
		(
			site_ods_code,
			site_party_key,
			site_asid,
			site_unique_identifier,
			added_date,
			site_definition_status_id,
			site_interactions
		)
		values
		(
			_site_ods_code,
			_site_party_key,
			_site_asid,
			_site_unique_identifier,
			now(),
			_site_definition_status,
			_site_interactions
		)
		returning
			application.site_definition.site_definition_id
		into
			_site_definition_id;
	else
		if
		(
			_site_ods_code = '' and
			_site_party_key = '' and
			_site_asid = ''
		)
		then
			insert into application.site_definition
			(
				site_ods_code,
				site_party_key,
				site_asid,
				site_unique_identifier,
				added_date,
				site_definition_status_id
			)
			values
			(
				_site_ods_code,
				_site_party_key,
				_site_asid,
				_site_unique_identifier,
				now(),
				_site_definition_status
			)
			returning
				application.site_definition.site_definition_id
			into
				_site_definition_id;
		else
			select into
				_site_definition_id sd.site_definition_id
			from
				application.site_definition sd
			where
				sd.site_ods_code = _site_ods_code
				and sd.site_party_key = _site_party_key
				and sd.site_asid = _site_asid;
		end if;
	end if;

	return query
	select
		sd.site_ods_code,
		sd.site_party_key,
		sd.site_asid,
		sd.site_unique_identifier,
		sd.site_definition_id,
		sd.site_definition_status_id,
		sd.site_interactions
	from
		application.site_definition sd
	where
		sd.site_definition_id = _site_definition_id;
end;
$$;


ALTER FUNCTION application.add_or_update_user(_email_address character varying, _is_admin boolean) OWNER TO postgres;

--
-- Name: FUNCTION add_or_update_user(_email_address character varying, _is_admin boolean); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.add_or_update_user(_email_address character varying, _is_admin boolean) TO app_user;
