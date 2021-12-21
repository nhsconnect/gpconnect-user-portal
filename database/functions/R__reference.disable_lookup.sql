drop function if exists reference.disable_lookup;

create function reference.disable_lookup
(
	_lookup_id integer,
	_disable_date timestamp default now()
)
returns void
as $$
begin
	update 
		reference.lookup
	set
		disabled_date = _disable_date
	where
		lookup_id = _lookup_id;
end;
$$ language plpgsql;

