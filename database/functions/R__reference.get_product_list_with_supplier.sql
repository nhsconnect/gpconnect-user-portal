drop function if exists reference.get_product_list_with_supplier;

create function reference.get_product_list_with_supplier
(
)
returns table
(
	lookup_id integer,
	lookup_type_id smallint,
	lookup_value text,
	lookup_type_name varchar(200),
	lookup_type_description varchar(200)
)
as $$
begin
	return query
	select 
		l1.lookup_id,
		l1.lookup_type_id,
		l1.lookup_value ||' - '|| l2.lookup_value, 
		lt.lookup_type_name,
		lt.lookup_type_description
	from
		reference.supplier_product sp
		inner join reference.lookup l1 on sp.supplier_product_id = l1.lookup_id
		inner join reference.lookup l2 on sp.supplier_id = l2.lookup_id
		inner join reference.lookup_type lt on l1.lookup_type_id = lt.lookup_type_id
	where
		l1.lookup_type_id = 3 
		and l2.lookup_type_id = 2
		and now() <= coalesce(l1.disabled_date, now())
	order by
		l1.lookup_value;
end;
$$ language plpgsql;