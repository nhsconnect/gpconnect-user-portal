drop function if exists application.update_site_definition_status;

create function application.update_site_definition_status
(
	_site_unique_identifier uuid,
	_site_definition_status_id integer
)
returns void
as $$
begin
	update
		application.site_definition sd
	set
		site_definition_status_id = _site_definition_status_id,
		last_updated = now()
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;