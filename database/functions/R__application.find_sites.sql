CREATE FUNCTION application.find_sites(
  _site_ods_code character varying DEFAULT NULL::character varying,
  _site_name character varying DEFAULT NULL::character varying,
  _start_position bigint DEFAULT 1,
  _number_to_return bigint DEFAULT 2147483647
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
  use_case_description character varying,
	site_row_number bigint
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select * from (
		select 
			v.*,
			row_number () over (order by v.sitename)
		from
			application.view_find_sites v
		where
			(_site_ods_code is null or v.site_ods_code ~* _site_ods_code) and
			(_site_name is null or v.sitename ~* _site_name)			
		order by 
			v.sitename
		) X 
	where
		row_number between _start_position and (_start_position + _number_to_return) - 1;
end;
$$;