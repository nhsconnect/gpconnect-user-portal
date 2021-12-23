drop function if exists application.get_site_definition;

create function application.get_site_definition
(
	_site_unique_identifier uuid
)
returns table
(
	site_asid varchar(50),
	site_party_key varchar(50),
	site_ods_code varchar(50),
	site_definition_id integer,
	site_unique_identifier uuid,
	site_definition_status_id smallint
)
as $$
begin
	return query
	select
		sd.site_asid,
		sd.site_party_key,
		sd.site_ods_code,
		sd.site_definition_id,
		sd.site_unique_identifier,
		sd.site_definition_status_id
	from 
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;