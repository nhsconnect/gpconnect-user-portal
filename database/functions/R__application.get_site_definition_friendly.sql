drop function if exists application.get_site_definition_friendly;

create function application.get_site_definition_friendly
(
	_site_unique_identifier uuid
)
returns table
(
	"Site ASID" varchar(50),
	"Site Party Key" varchar(50),
	"Site ODS Code" varchar(50)
)
as $$
begin
	return query
	select
		sd.site_asid,
		sd.site_party_key,
		sd.site_ods_code
	from 
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;