--
-- Name: update_site_definition(uuid, character varying, character varying, character varying); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.update_site_definition(
  _site_unique_identifier uuid,
  _site_ods_code character varying DEFAULT NULL::character varying,
  _site_party_key character varying DEFAULT NULL::character varying,
  _site_asid character varying DEFAULT NULL::character varying
) RETURNS TABLE(
  site_ods_code character varying,
  site_party_key character varying,
  site_asid character varying,
  site_unique_identifier uuid,
  site_definition_id integer
)
    LANGUAGE plpgsql
    AS $$
begin
	update
		application.site_definition sd
	set
		site_ods_code = coalesce(_site_ods_code, sd.site_ods_code),
		site_party_key = coalesce(_site_party_key, sd.site_party_key),
		site_asid = coalesce(_site_asid, sd.site_asid),
		last_updated = now()
	where
		sd.site_unique_identifier = _site_unique_identifier;

	return query
	select
		sd.site_ods_code,
		sd.site_party_key,
		sd.site_asid,
		sd.site_unique_identifier,
		sd.site_definition_id
	from
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$;


ALTER FUNCTION application.update_site_definition(
  _site_unique_identifier uuid,
  _site_ods_code character varying,
  _site_party_key character varying,
  _site_asid character varying
) OWNER TO postgres;

--
-- Name: FUNCTION update_site_definition(_site_unique_identifier uuid, _site_ods_code character varying, _site_party_key character varying, _site_asid character varying); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.update_site_definition(
  _site_unique_identifier uuid,
  _site_ods_code character varying,
  _site_party_key character varying,
  _site_asid character varying
) TO app_user;


