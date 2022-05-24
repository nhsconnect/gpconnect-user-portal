--
-- Name: get_lookup_types(); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_lookup_types() RETURNS TABLE(
  lookup_type_id integer,
  lookup_type_name character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		lt.lookup_type_id,
		lt.lookup_type_name
	from reference.lookup_type lt;
end;
$$;


ALTER FUNCTION reference.get_lookup_types() OWNER TO postgres;

--
-- Name: FUNCTION get_lookup_types(); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_lookup_types() TO app_user;


