drop function if exists reference.update_lookup;

create function reference.update_lookup
(
	_lookup_id smallint,
	_lookup_value varchar(500)
)
returns void
as $$
begin
	update 
		reference.lookup
	set
		lookup_value = _lookup_value
	where
		lookup_id = _lookup_id;
end;
$$ language plpgsql;