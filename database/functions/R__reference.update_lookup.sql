drop function if exists reference.update_lookup;

create function reference.update_lookup
(
	_lookup_id smallint,
	_lookup_value text,
	_lookup_type_id smallint
)
returns void
as $$
begin
	update
		reference.lookup
	set
		lookup_value = _lookup_value
	where
		lookup_id = _lookup_id
		and reference.lookup.lookup_type_id = _lookup_type_id;
end;
$$ language plpgsql;
