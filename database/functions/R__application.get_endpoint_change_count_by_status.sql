--
-- Name: get_endpoint_change_count_by_status(); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_endpoint_change_count_by_status() RETURNS TABLE(
  site_definition_status_id smallint,
  site_definition_status_name character varying,
  site_definition_status_count bigint
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
		select
			sd.site_definition_status_id,
			sds.site_definition_status_name,
			count(*) site_definition_status_count
		from
			application.site_definition sd
			inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
		group by
			sd.site_definition_status_id,
			sds.site_definition_status_name
		order by
			sds.site_definition_status_name;
end;
$$;


ALTER FUNCTION application.get_endpoint_change_count_by_status() OWNER TO postgres;

--
-- Name: FUNCTION get_endpoint_change_count_by_status(); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_endpoint_change_count_by_status() TO app_user;

