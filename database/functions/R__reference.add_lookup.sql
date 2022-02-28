drop function if exists reference.add_lookup;

create function reference.add_lookup
(
	_lookup_type_id integer,
	_lookup_value varchar(500),
	_linked_lookup_id integer default null
)
returns void
as $$
begin
	insert into reference.lookup(lookup_value, lookup_type_id, added_date, linked_lookup_id)
	values (_lookup_value, _lookup_type_id, now(), _linked_lookup_id);
end;
$$ language plpgsql;
