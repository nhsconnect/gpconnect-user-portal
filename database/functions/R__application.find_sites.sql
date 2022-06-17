--
-- Name: find_sites(smallint, smallint, character varying, character varying, character varying, character varying, bigint, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: application; Owner: postgres
--

CREATE FUNCTION application.find_sites(
  _site_definition_status_min smallint,
  _site_definition_status_max smallint,
  _html_query_filter_interaction character varying,
  _structured_query_filter_interaction character varying,
  _appointment_query_filter_interaction character varying,
  _send_document_query_filter_interaction character varying,
  _filter_by bigint DEFAULT NULL::bigint,
  _site_ods_code character varying DEFAULT NULL::character varying,
  _site_name character varying DEFAULT NULL::character varying,
  _ccg_ods_code character varying DEFAULT NULL::character varying,
  _ccg_name character varying DEFAULT NULL::character varying
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
			a.siteaddressline1,
			a.siteaddressline2,
			a.siteaddresstown,
			a.siteaddresscounty,
			a.siteaddresscountry,
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
			where
				sd.site_definition_status_id >= _site_definition_status_min
				and sd.site_definition_status_id <= _site_definition_status_max
			group by
				sd.site_definition_id, sds.site_definition_status_name
			order by
				sd.site_definition_id
		) a
		where
			(_site_ods_code is null or a.site_ods_code ~* _site_ods_code) and
			(_site_name is null or a.sitename ~* _site_name) and
			(_ccg_ods_code is null or position(_ccg_ods_code in a.site_attributes_json->>'SelectedCCGOdsCode') > 0) and
			(_ccg_name is null or position(_ccg_name in a.site_attributes_json->>'SelectedCCGName') > 0)
			) b
		where case
			when _filter_by is null then 1=1
			when _filter_by = 0 then 1=1
			when _filter_by = 1 then position(_html_query_filter_interaction in b.site_interactions) = 0
			when _filter_by = 2 then position(_html_query_filter_interaction in b.site_interactions) > 0
			when _filter_by = 3 then position(_structured_query_filter_interaction in b.site_interactions) = 0
			when _filter_by = 4 then position(_structured_query_filter_interaction in b.site_interactions) > 0
			when _filter_by = 5 then position(_appointment_query_filter_interaction in b.site_interactions) = 0
			when _filter_by = 6 then position(_appointment_query_filter_interaction in b.site_interactions) > 0
			when _filter_by = 7 then position(_send_document_query_filter_interaction in b.site_interactions) = 0
			when _filter_by = 8 then position(_send_document_query_filter_interaction in b.site_interactions) > 0
		end;
end;
$$;


ALTER FUNCTION application.find_sites(
  _site_definition_status_min smallint,
  _site_definition_status_max smallint,
  _html_query_filter_interaction character varying,
  _structured_query_filter_interaction character varying,
  _appointment_query_filter_interaction character varying,
  _send_document_query_filter_interaction character varying,
  _filter_by bigint,
  _site_ods_code character varying,
  _site_name character varying,
  _ccg_ods_code character varying,
  _ccg_name character varying
) OWNER TO postgres;

