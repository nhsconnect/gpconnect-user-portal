--
-- Name: get_organisation(character varying); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_organisation(
  _ods_code character varying
) RETURNS TABLE(
  organisation_id integer,
  organisation_name character varying,
  ods_code character varying,
  org_status character varying,
  org_record_class character varying,
  postcode character varying,
  last_change_date timestamp without time zone,
  primary_role_id character varying,
  primary_role_description character varying,
  organisation_link character varying,
  added_date timestamp without time zone,
  last_sync_date timestamp without time zone
)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select
		o.organisation_id,
		o.organisation_name,
		o.ods_code,
		o.org_status,
		o.org_record_class,
		o.postcode,
		o.last_change_date,
		o.primary_role_id,
		o.primary_role_description,
		o.organisation_link,
		o.added_date,
		o.last_sync_date
	from reference.organisation o
	where o.ods_code = _ods_code;
end;
$$;


ALTER FUNCTION reference.get_organisation(
  _ods_code character varying
) OWNER TO postgres;

--
-- Name: FUNCTION get_organisation(_ods_code character varying); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_organisation(
  _ods_code character varying
) TO app_user;


