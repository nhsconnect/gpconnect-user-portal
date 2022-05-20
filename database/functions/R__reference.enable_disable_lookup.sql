--
-- Name: enable_disable_lookup(smallint, boolean); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.enable_disable_lookup(
  _lookup_id smallint,
  _is_disabled boolean
) RETURNS void
    LANGUAGE plpgsql
    AS $$
begin
	update
		reference.lookup
	set
		disabled_date = case when _is_disabled then now() else null end
	where
		lookup_id = _lookup_id;
end;
$$;


ALTER FUNCTION reference.enable_disable_lookup(
  _lookup_id smallint,
  _is_disabled boolean
) OWNER TO postgres;

--
-- Name: FUNCTION enable_disable_lookup(_lookup_id smallint, _is_disabled boolean); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.enable_disable_lookup(
  _lookup_id smallint,
  _is_disabled boolean
) TO app_user;


