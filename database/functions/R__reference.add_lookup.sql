drop function if exists reference.add_lookup;

create function reference.add_lookup
(
	_lookup_type_id integer,
	_lookup_value varchar(500)
)
returns void
as $$
begin
	insert into reference.lookup(lookup_value, lookup_type_id, added_date)
	values (_lookup_value, _lookup_type_id, now());
end;
$$ language plpgsql;