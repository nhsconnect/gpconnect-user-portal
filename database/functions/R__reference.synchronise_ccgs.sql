--
-- Name: synchronise_ccgs(character varying, character varying); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.synchronise_ccgs(
  _ods_code character varying,
  _organisation_name character varying
) RETURNS void
    LANGUAGE plpgsql
    AS $$
declare	_linked_lookup_id integer;
begin
	if not exists
	(
		select
			*
		from
			reference.lookup l
		where
			l.lookup_value = _organisation_name
			and l.lookup_type_id = 6
	)
	then
		perform reference.add_lookup(6, _organisation_name);
	end if;

	select into
		_linked_lookup_id l.lookup_id
	from
		reference.lookup l
	where
		l.lookup_value = _organisation_name
		and lookup_type_id = 6;

	if not exists
	(
		select
			*
		from
			reference.lookup l
		where
			l.lookup_value = _ods_code
			and l.lookup_type_id = 5
	)
	then
		perform reference.add_lookup(5, _ods_code, _linked_lookup_id);
	end if;
end;
$$;


ALTER FUNCTION reference.synchronise_ccgs(
  _ods_code character varying,
  _organisation_name character varying
) OWNER TO postgres;

--
-- Name: FUNCTION synchronise_ccgs(_ods_code character varying, _organisation_name character varying); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.synchronise_ccgs(
  _ods_code character varying,
  _organisation_name character varying
) TO app_user;


