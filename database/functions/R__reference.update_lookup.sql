--
-- Name: update_lookup(smallint, character varying); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.update_lookup(
  _lookup_id smallint,
  _lookup_value character varying
) RETURNS void
    LANGUAGE plpgsql
    AS $$
begin
	update
		reference.lookup
	set
		lookup_value = _lookup_value
	where
		lookup_id = _lookup_id;
end;
$$;


ALTER FUNCTION reference.update_lookup(
  _lookup_id smallint,
  _lookup_value character varying
) OWNER TO postgres;

--
-- Name: FUNCTION update_lookup(_lookup_id smallint, _lookup_value character varying); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.update_lookup(
  _lookup_id smallint,
  _lookup_value character varying
) TO app_user;


