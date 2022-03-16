drop function if exists reference.get_lookup_type;

create function reference.get_lookup_type
(
	_lookup_type_id integer
)
returns table
(
	lookup_type_id integer,
	lookup_type_name varchar(200),
	lookup_type_description varchar(200)
)
as $$
begin
	return query
	select
		lt.lookup_type_id,
		lt.lookup_type_name,
		lt.lookup_type_description
	from
		reference.lookup_type lt
	where
		lt.lookup_type_id = _lookup_type_id;
end;
$$ language plpgsql;