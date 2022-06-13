drop function if exists reference.enable_disable_lookup;

create function reference.enable_disable_lookup
(
	_lookup_id smallint,
	_is_disabled boolean,
	_lookup_type_id smallint
)
returns void
as $$
begin
	update
		reference.lookup
	set
		disabled_date = case when _is_disabled then now() else null end
	where
		lookup_id = _lookup_id
		and reference.lookup.lookup_type_id = _lookup_type_id;
end;
$$ language plpgsql;

