drop function if exists reference.get_lookup_types;

create function reference.get_lookup_types
(
)
returns table
(
	lookup_type_id integer,
	lookup_type_name varchar(200)
)
as $$
begin
	return query
	select
		lt.lookup_type_id,
		lt.lookup_type_name
	from reference.lookup_type lt;
end;
$$ language plpgsql;