--
-- Name: get_endpoint_changes(smallint, smallint, timestamp with time zone, timestamp with time zone, text); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_endpoint_changes(
  _site_definition_status_id_lower_band smallint,
  _site_definition_status_id_upper_band smallint,
  _search_date_from timestamp with time zone,
  _search_date_to timestamp with time zone,
  _search_value text DEFAULT NULL::text
) RETURNS TABLE(
  submitted_date timestamp without time zone,
  site_definition_status_name character varying,
  site_definition_id integer,
  site_unique_identifier uuid,
  site_definition_status_id smallint,
  site_name text
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		a.added_date,
		a.site_definition_status_name,
		a.site_definition_id,
		a.site_unique_identifier,
		a.site_definition_status_id,
		a.site_name
	from (
		select
			coalesce(sd.last_updated, sd.added_date) added_date,
			sds.site_definition_status_name,
			sd.site_definition_id,
			sd.site_unique_identifier,
			sd.site_definition_status_id,
			json_object_agg(sa.site_attribute_name, sa.site_attribute_value)->>'SiteName' site_name,
			sd.site_ods_code
		from
			application.site_definition sd
			inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
			inner join application.site_attribute sa on sd.site_definition_id = sa.site_definition_id
		where
			sd.site_definition_status_id in (_site_definition_status_id_lower_band, _site_definition_status_id_upper_band)
			and sd.added_date >= _search_date_from
			and sd.added_date <= _search_date_to
		group by
			sd.added_date,
			sds.site_definition_status_name,
			sd.site_definition_id,
			sd.site_unique_identifier,
			sd.site_definition_status_id
		) a
	where
		_search_value is null or (a.site_name ~* _search_value or a.site_ods_code ~* _search_value)
	order by
		a.site_name;
end;
$$;


ALTER FUNCTION application.get_endpoint_changes(
  _site_definition_status_id_lower_band smallint,
  _site_definition_status_id_upper_band smallint,
  _search_date_from timestamp with time zone,
  _search_date_to timestamp with time zone,
  _search_value text
) OWNER TO postgres;

--
-- Name: FUNCTION get_endpoint_changes(_site_definition_status_id_lower_band smallint, _site_definition_status_id_upper_band smallint, _search_date_from timestamp with time zone, _search_date_to timestamp with time zone, _search_value text); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_endpoint_changes(
  _site_definition_status_id_lower_band smallint,
  _site_definition_status_id_upper_band smallint,
  _search_date_from timestamp with time zone,
  _search_date_to timestamp with time zone,
  _search_value text
) TO app_user;

