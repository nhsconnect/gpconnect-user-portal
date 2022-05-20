--
-- Name: update_site_definition_status(uuid, integer); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.update_site_definition_status(
  _site_unique_identifier uuid,
  _site_definition_status_id integer
) RETURNS void
    LANGUAGE plpgsql
    AS $$
begin
	update
		application.site_definition sd
	set
		site_definition_status_id = _site_definition_status_id,
		last_updated = now()
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$;


ALTER FUNCTION application.update_site_definition_status(
  _site_unique_identifier uuid,
  _site_definition_status_id integer
) OWNER TO postgres;

--
-- Name: FUNCTION update_site_definition_status(_site_unique_identifier uuid, _site_definition_status_id integer); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.update_site_definition_status(
  _site_unique_identifier uuid,
  _site_definition_status_id integer
) TO app_user;


