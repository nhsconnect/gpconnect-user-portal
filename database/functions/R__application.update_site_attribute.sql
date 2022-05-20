--
-- Name: update_site_attribute(uuid, character varying, character varying); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.update_site_attribute(
  _site_unique_identifier uuid,
  _site_attribute_name character varying,
  _site_attribute_value character varying
) RETURNS TABLE(
  site_attribute_id integer,
  site_definition_id integer,
  site_unique_identifier uuid,
  site_attribute_name character varying,
  site_attribute_value character varying,
  lookup_value character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	update
		application.site_attribute sa
	set
		site_attribute_value = _site_attribute_value,
		last_updated = now()
	from
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier
		and sa.site_definition_id = sd.site_definition_id
		and sa.site_attribute_name = _site_attribute_name
		and sa.site_attribute_value != _site_attribute_value;

	return query
	select
		sa.site_attribute_id,
		sa.site_definition_id,
		sd.site_unique_identifier,
		sa.site_attribute_name,
		sa.site_attribute_value,
		l.lookup_value
	from
		application.site_attribute sa
	inner join
		application.site_definition sd on sa.site_definition_id = sd.site_definition_id
	left outer join
		reference.lookup l on sa.site_attribute_value = l.lookup_id::varchar
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$;


ALTER FUNCTION application.update_site_attribute(_site_unique_identifier uuid, _site_attribute_name character varying, _site_attribute_value character varying) OWNER TO postgres;

--
-- Name: FUNCTION update_site_attribute(_site_unique_identifier uuid, _site_attribute_name character varying, _site_attribute_value character varying); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.update_site_attribute(
  _site_unique_identifier uuid,
  _site_attribute_name character varying,
  _site_attribute_value character varying
) TO app_user;


