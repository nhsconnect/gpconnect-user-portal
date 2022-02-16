drop function if exists reference.get_lookup_data_count_by_type;

create function reference.get_lookup_data_count_by_type
(	
)
returns table
(
	lookup_type_id smallint,
	lookup_type_name varchar(200),
	lookup_type_description varchar(200),
	lookup_type_count bigint
)
as $$
begin
	return query
		select			
			l.lookup_type_id,
			lt.lookup_type_name,
			lt.lookup_type_description,
			count(*) lookup_type_count
		from 
			reference.lookup l
			inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
		group by
			l.lookup_type_id,
			lt.lookup_type_name,
			lt.lookup_type_description
		order by
			lt.lookup_type_description;
end;
$$ language plpgsql;