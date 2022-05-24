--
-- Name: get_site_definition(uuid); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_site_definition(
  _site_unique_identifier uuid
) RETURNS TABLE(
  site_asid character varying,
  site_party_key character varying,
  site_ods_code character varying,
  site_definition_id integer,
  site_unique_identifier uuid,
  site_definition_status_id smallint,
  site_definition_status_name character varying,
  submitted_date timestamp without time zone
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		sd.site_asid,
		sd.site_party_key,
		sd.site_ods_code,
		sd.site_definition_id,
		sd.site_unique_identifier,
		sd.site_definition_status_id,
		sds.site_definition_status_name,
		coalesce(sd.last_updated, sd.added_date) submitted_date
	from
		application.site_definition sd
		inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$;


ALTER FUNCTION application.get_site_definition(
  _site_unique_identifier uuid
) OWNER TO postgres;

--
-- Name: FUNCTION get_site_definition(_site_unique_identifier uuid); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_site_definition(
  _site_unique_identifier uuid
) TO app_user;


