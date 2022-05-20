--
-- Name: get_organisations(); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_organisations() RETURNS TABLE(
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
	from reference.organisation o;
end;
$$;


ALTER FUNCTION reference.get_organisations() OWNER TO postgres;

--
--
-- Name: FUNCTION get_organisations(); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.get_organisations() TO app_user;


