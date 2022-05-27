--
-- Name: synchronise_organisation(character varying, character varying, character varying, character varying, timestamp without time zone, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.synchronise_organisation(
  _ods_code character varying,
  _organisation_name character varying,
  _org_status character varying,
  _org_record_class character varying,
  _last_change_date timestamp without time zone,
  _primary_role_id character varying,
  _primary_role_description character varying,
  _organisation_link character varying,
  _postcode character varying
) RETURNS void
    LANGUAGE plpgsql
    AS $$
begin
	if not exists
	(
		select
			*
		from reference.organisation o
		where o.ods_code = _ods_code
	)
	then
		insert into reference.organisation
		(
			organisation_name,
			ods_code,
			org_status,
			org_record_class,
			postcode,
			last_change_date,
			primary_role_id,
			primary_role_description,
			organisation_link,
			added_date,
			last_sync_date
		)
		values
		(
			_organisation_name,
			_ods_code,
			_org_status,
			_org_record_class,
			_postcode,
			_last_change_date,
			_primary_role_id,
			_primary_role_description,
			_organisation_link,
			now(),
			now()
		);
	else
		update application.organisation
		set
			organisation_name = _organisation_name,
			ods_code = _ods_code,
			org_status = _org_status,
			org_record_class = _org_record_class,
			postcode = _postcode,
			last_change_date = _last_change_date,
			primary_role_id = _primary_role_id,
			primary_role_description = _primary_role_description,
			organisation_link = _organisation_link,
			last_sync_date = now()
		where ods_code = _ods_code;
	end if;
end;
$$;


ALTER FUNCTION reference.synchronise_organisation(
  _ods_code character varying,
  _organisation_name character varying,
  _org_status character varying,
  _org_record_class character varying,
  _last_change_date timestamp without time zone,
  _primary_role_id character varying,
  _primary_role_description character varying,
  _organisation_link character varying,
  _postcode character varying
) OWNER TO postgres;

--
-- Name: FUNCTION synchronise_organisation(_ods_code character varying, _organisation_name character varying, _org_status character varying, _org_record_class character varying, _last_change_date timestamp without time zone, _primary_role_id character varying, _primary_role_description character varying, _organisation_link character varying, _postcode character varying); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.synchronise_organisation(
  _ods_code character varying,
  _organisation_name character varying,
  _org_status character varying,
  _org_record_class character varying,
  _last_change_date timestamp without time zone,
  _primary_role_id character varying,
  _primary_role_description character varying,
  _organisation_link character varying,
  _postcode character varying
) TO app_user;


