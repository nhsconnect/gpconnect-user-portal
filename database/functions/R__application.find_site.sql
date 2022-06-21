CREATE FUNCTION application.find_site(
  _site_unique_identifier uuid
) RETURNS TABLE(
  site_definition_id integer,
  site_ods_code character varying,
  site_unique_identifier uuid,
  site_interactions character varying,
  site_name character varying,
  selected_ccg_ods_code character varying,
  selected_ccg_name character varying,
  is_appointment_enabled boolean,
  is_html_enabled boolean,
  is_structured_enabled boolean,
  is_send_document_enabled boolean,
  site_address_line_1 character varying,
  site_address_line_2 character varying,
  site_address_town character varying,
  site_address_county character varying,
  site_address_country character varying,
  site_postcode character varying,
  ods_code character varying,
  selected_supplier character varying,
  use_case_description character varying
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
		select 
			v.*
		from
			application.view_find_sites v
		where
			v.site_unique_identifier = _site_unique_identifier;
end;
$$;