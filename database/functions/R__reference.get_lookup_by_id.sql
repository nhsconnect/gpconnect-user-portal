--
-- Name: get_lookup_by_id(integer); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_lookup_by_id(
  _lookup_id integer
) RETURNS TABLE(
  lookup_id integer,
  lookup_type_id smallint,
  lookup_value character varying,
  linked_lookup_id integer,
  lookup_type_name character varying,
  lookup_type_description character varying,
  linked_lookup_value character varying,
  is_disabled boolean
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
		l2.lookup_value,
		case when l.disabled_date is null then false else true end is_disabled
	from reference.lookup l
	left outer join reference.lookup l2 on l.linked_lookup_id = l2.lookup_id
	inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
	where l.lookup_id = _lookup_id;
end;
$$;


ALTER FUNCTION reference.get_lookup_by_id(
  _lookup_id integer
) OWNER TO postgres;

--
-- Name: FUNCTION get_lookup_by_id(_lookup_id integer); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_lookup_by_id(
  _lookup_id integer
) TO app_user;


