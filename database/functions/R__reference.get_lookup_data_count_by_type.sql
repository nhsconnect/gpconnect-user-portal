--
-- Name: get_lookup_data_count_by_type(); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_lookup_data_count_by_type() RETURNS TABLE(
  lookup_type_id smallint,
  lookup_type_name character varying,
  lookup_type_description character varying,
  lookup_type_count bigint
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
		select
			l.lookup_type_id,
			lt.lookup_type_name,
			lt.lookup_type_description,
			count(*) lookup_type_count
		from
			reference.lookup l
			inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
		where
			lt.is_system = false
		group by
			l.lookup_type_id,
			lt.lookup_type_name,
			lt.lookup_type_description
		order by
			lt.lookup_type_description;
end;
$$;


ALTER FUNCTION reference.get_lookup_data_count_by_type() OWNER TO postgres;

--
-- Name: FUNCTION get_lookup_data_count_by_type(); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_lookup_data_count_by_type() TO app_user;


