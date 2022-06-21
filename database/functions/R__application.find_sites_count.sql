CREATE FUNCTION application.find_sites_count(
  _site_ods_code character varying DEFAULT NULL::character varying,
  _site_name character varying DEFAULT NULL::character varying
) RETURNS bigint
    LANGUAGE plpgsql
    AS $$
begin
	RETURN (select
		count(*) 
	from 
		application.find_sites(_site_ods_code, _site_name));
end;
$$;