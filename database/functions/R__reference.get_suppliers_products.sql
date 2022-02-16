drop function if exists reference.get_suppliers_products;

create function reference.get_suppliers_products
(
)
returns table
(
	supplier_name varchar(500),
	supplier_id smallint,
	supplier_product_id smallint,
	product_name varchar(500),
	lookup_type_id smallint,
	provider_assured boolean,
	consumer_assured boolean
)
as $$
begin
	return query
	select 
		l.lookup_value as supplier_name, 
		a.supplier_id, 
		a.supplier_product_id, 
		a.product_name, 
		a.lookup_type_id,
		a.provider_assured,
		a.consumer_assured
	from (
		select 
			sp.supplier_id, 
			sp.supplier_product_id, 
			l.lookup_value as product_name, 
			l.lookup_type_id,
			sp.provider_assured,
			sp.consumer_assured
		from
			reference.supplier_product sp
		inner join 
			reference.lookup l on sp.supplier_product_id = l.lookup_id
		where
			l.lookup_type_id in (select l.lookup_type_id from reference.lookup_type l where l.lookup_type_name='SupplierProduct')
			and now() <= coalesce(l.disabled_date, now())
	) a 
	inner join 
		reference.lookup l on a.supplier_id = l.lookup_id
	order by 
		supplier_name, product_name;
end;
$$ language plpgsql;