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
 		value character varying(500)
	);       
   END;
$$;

ALTER FUNCTION application.add_site_attributes(_site_unique_identifier uuid, json) OWNER TO postgres;

GRANT ALL ON FUNCTION application.add_site_attributes(_site_unique_identifier uuid, json) TO app_user;

/*
SELECT application.add_site_attributes('5d878eaa-7982-4e05-bf6f-98845789de03',
	'[
        {"name": "OdsCode", "value": "X26"}, 
        {"name": "SiteName", "value": "NHS DIGITAL"},
		{"name": "SiteAddressLine1", "value": "THE LEEDS GOVERNMENT HUB"},
		{"name": "SiteAddressLine2", "value": "7-8 WELLINGTON PLACE"},
		{"name": "SiteAddressTown", "value": "LEEDS"},
		{"name": "SiteAddressCounty", "value": "WEST YORKSHIRE"},
		{"name": "SiteAddressCountry", "value": "ENGLAND"},
		{"name": "SitePostcode", "value": "LS1 4AP"},
		{"name": "UseCaseDescription", "value": "Example Use Case Description"},
		{"name": "SoftwareSupplierName", "value": "DXC Lorenzo"},
		{"name": "SignatoryName", "value": "Joe Bloggs"},
		{"name": "SignatoryEmail", "value": "joebloggs@nhs.net"},
		{"name": "SignatoryPosition", "value": "CEO"},
		{"name": "IsAppointmentEnabled", "value": "true"},
		{"name": "IsHtmlEnabled", "value": "true"},
		{"name": "IsStructuredEnabled", "value": "true"},
		{"name": "IsSendDocumentEnabled", "value": "true"}
    ]'
);
*/
