--
-- Name: get_site_definition_friendly(uuid); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_site_definition_friendly(
  _site_unique_identifier uuid
) RETURNS TABLE(
  siteasid character varying,
  sitepartykey character varying,
  odscode character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		sd.site_asid,
		sd.site_party_key,
		sd.site_ods_code
	from
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$;


ALTER FUNCTION application.get_site_definition_friendly(
  _site_unique_identifier uuid
) OWNER TO postgres;

--
-- Name: FUNCTION get_site_definition_friendly(_site_unique_identifier uuid); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_site_definition_friendly(
  _site_unique_identifier uuid
) TO app_user;


