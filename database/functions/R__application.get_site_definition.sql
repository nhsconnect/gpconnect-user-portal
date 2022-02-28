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
	site_definition_status_id smallint,
	site_definition_status_name varchar(100),
	submitted_date timestamp without time zone
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
		sd.site_definition_status_id,
		sds.site_definition_status_name,
		coalesce(sd.last_updated, sd.added_date) submitted_date
	from 
		application.site_definition sd
		inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;