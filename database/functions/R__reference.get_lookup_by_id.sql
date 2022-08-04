drop function if exists reference.get_lookup_by_id;

create function reference.get_lookup_by_id
(
	_lookup_id integer,
	_lookup_type_id smallint
)
returns table
(
	lookup_id integer,
	lookup_type_id smallint,
	lookup_value text,
	linked_lookup_id integer,
	lookup_type_name varchar(200),
	lookup_type_description varchar(200),
	linked_lookup_value text,
	is_disabled boolean
)
as $$
begin
	return query
	select
		l.lookup_id,
		l.lookup_type_id,
		l.lookup_value,
		l.linked_lookup_id,
		lt.lookup_type_name,
		lt.lookup_type_description,
		l2.lookup_value,
		case when l.disabled_date is null then false else true end is_disabled
	from reference.lookup l
	left outer join reference.lookup l2 on l.linked_lookup_id = l2.lookup_id
	inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
	where l.lookup_id = _lookup_id
	and l.lookup_type_id = _lookup_type_id;
end;
$$ language plpgsql;
