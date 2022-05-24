--
-- Name: get_lookup_type(integer); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_lookup_type(
  _lookup_type_id integer
) RETURNS TABLE(
  lookup_type_id integer,
  lookup_type_name character varying,
  lookup_type_description character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		lt.lookup_type_id,
		lt.lookup_type_name,
		lt.lookup_type_description
	from
		reference.lookup_type lt
	where
		lt.lookup_type_id = _lookup_type_id;
end;
$$;


ALTER FUNCTION reference.get_lookup_type(
  _lookup_type_id integer
) OWNER TO postgres;

