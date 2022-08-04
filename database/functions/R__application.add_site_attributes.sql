CREATE OR REPLACE FUNCTION application.add_site_attributes(_site_unique_identifier uuid, _site_attributes text)
RETURNS void
LANGUAGE 'plpgsql'
AS $$
declare	_site_definition_id integer;
BEGIN
	select into
		_site_definition_id sd.site_definition_id
	from
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;

	insert into application.site_attribute (site_definition_id, site_attribute_name, site_attribute_value, added_date)
	select
		_site_definition_id,
		x.name,
		x.value,
		now()
	from json_to_recordset(_site_attributes::json) x
	(
    	name character varying(100),
 		value text
	);
   END;
$$;

ALTER FUNCTION application.add_site_attributes(_site_unique_identifier uuid, _site_attributes text) OWNER TO postgres;

GRANT ALL ON FUNCTION application.add_site_attributes(_site_unique_identifier uuid, _site_attributes text) TO app_user;
