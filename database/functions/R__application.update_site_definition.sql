drop function if exists application.update_site_definition;

create function application.update_site_definition
(
	_site_unique_identifier uuid,
	_site_ods_code varchar(50),
	_site_party_key varchar(50),
	_site_asid varchar(50)
)
returns table
(
	site_ods_code varchar(50),
	site_party_key varchar(50),
	site_asid varchar(50),
	site_unique_identifier uuid,
	site_definition_id integer
)
as $$
begin
	update
		application.site_definition sd
	set
		site_ods_code = _site_ods_code,
		site_party_key = _site_party_key,
		site_asid = _site_asid,
		last_updated = now()
	where
		sd.site_unique_identifier = _site_unique_identifier;
		
	return query
	select
		sd.site_ods_code, 
		sd.site_party_key,
		sd.site_asid, 
		sd.site_unique_identifier,
		sd.site_definition_id
	from 
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;