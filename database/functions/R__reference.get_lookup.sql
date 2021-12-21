drop function if exists reference.get_lookup;

create function reference.get_lookup
(
	_lookup_type_id integer
)
returns table
(
	lookup_id integer,
	lookup_type_id smallint,
	lookup_value varchar(500),
	lookup_type_name varchar(200)
)
as $$
begin
	return query
	select
		l.lookup_id,
		l.lookup_type_id,
		l.lookup_value,
		lt.lookup_type_name
	from reference.lookup l
	inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
	where lt.lookup_type_id = _lookup_type_id and now() <= coalesce(l.disabled_date, now())
	order by l.lookup_value;
end;
$$ language plpgsql;