--
-- Name: get_site_attributes(uuid); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.get_site_attributes(
  _site_unique_identifier uuid
) RETURNS TABLE (
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
	return query
	select
		sa.site_attribute_id,
		sa.site_definition_id,
		sd.site_unique_identifier,
		sa.site_attribute_name,
		sa.site_attribute_value site_attribute_value,
		l.lookup_value lookup_value
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


ALTER FUNCTION application.get_site_attributes(
  _site_unique_identifier uuid
) OWNER TO postgres;

--
-- Name: FUNCTION get_site_attributes(_site_unique_identifier uuid); Type: ACL; Schema: application; Owner: postgres
--

GRANT ALL ON FUNCTION application.get_site_attributes(
  _site_unique_identifier uuid
) TO app_user;

