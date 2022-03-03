drop function if exists reference.enable_disable_lookup;

create function reference.enable_disable_lookup
(
	_lookup_id smallint,
	_is_disabled boolean
)
returns void
as $$
begin
	update 
		reference.lookup
	set
		disabled_date = case when _is_disabled then now() else null end
	where
		lookup_id = _lookup_id;
end;
$$ language plpgsql;

