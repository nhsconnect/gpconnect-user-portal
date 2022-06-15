--
-- Name: find_site(uuid); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.find_site(
  _site_unique_identifier uuid
) RETURNS TABLE(
  site_definition_id integer,
  site_ods_code character varying,
  site_unique_identifier uuid,
  site_definition_status_id smallint,
  site_definition_status_name character varying,
  site_interactions character varying,
  site_name character varying,
  selected_ccg_ods_code character varying,
  selected_ccg_name character varying,
  is_appointment_enabled boolean,
  is_html_enabled boolean,
  is_structured_enabled boolean,
  is_send_document_enabled boolean,
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
		*
	from (
		select
			a.site_definition_id,
			a.site_ods_code,
			a.site_unique_identifier,
			a.site_definition_status_id,
			a.site_definition_status_name,
			a.site_interactions,
			a.sitename,
			a.selectedccgodscode,
			a.selectedccgname,
			a.isappointmentenabled,
			a.ishtmlenabled,
			a.isstructuredenabled,
			a.issenddocumentenabled,
			a.sitepostcode,
			a.odscode,
			a.selectedsupplier,
			a.usecasedescription
		from (
			select
				sd.site_definition_id,
				sd.site_ods_code,
				sd.site_unique_identifier,
				sd.site_definition_status_id,
				sds.site_definition_status_name,
				sd.site_interactions,
				json_object_agg(sa.site_attribute_name, sa.site_attribute_value) site_attributes_json,
				(json_populate_record(null::"application".site_attributes_type, json_object_agg(lower(sa.site_attribute_name), coalesce(l.lookup_value, sa.site_attribute_value)))).*
			from
				application.site_attribute sa
				inner join application.site_definition sd on sa.site_definition_id = sd.site_definition_id
				left outer join reference.lookup l on sa.site_attribute_value = l.lookup_id::varchar
				inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
			group by
				sd.site_definition_id, sds.site_definition_status_name
			order by
				sd.site_definition_id
		) a
	) b 
	where b.site_unique_identifier = _site_unique_identifier;
end;
$$;

ALTER FUNCTION application.find_site(
  _site_unique_identifier uuid
) OWNER TO postgres;

