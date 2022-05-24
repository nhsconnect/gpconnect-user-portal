--
-- Name: get_lookups(); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_lookups() RETURNS TABLE(
  lookup_id integer,
  lookup_type_id smallint,
  lookup_value character varying,
  linked_lookup_id integer,
  lookup_type_name character varying,
  lookup_type_description character varying,
  linked_lookup_value character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		l.lookup_id,
		l.lookup_type_id,
		l.lookup_value,
		l.linked_lookup_id,
		lt.lookup_type_name,
		lt.lookup_type_description,
		l2.lookup_value
	from reference.lookup l
	left outer join reference.lookup l2 on l.linked_lookup_id = l2.lookup_id
	inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
	where now() <= coalesce(l.disabled_date, now());
end;
$$;


ALTER FUNCTION reference.get_lookups() OWNER TO postgres;

--
-- Name: FUNCTION get_lookups(); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_lookups() TO app_user;


