drop function if exists reference.get_supplier_products;

create function reference.get_supplier_products
(
	_supplier_id integer
)
returns table
(
	supplier_name varchar(500),
	supplier_id smallint,
	supplier_product_id smallint,
	product_name varchar(500),
	lookup_type_id smallint
)
as $$
begin
	return query
	select 
	l.lookup_value as supplier_name,
	a.supplier_id,
	a.supplier_product_id, 
	a.product_name, 
	a.lookup_type_id 	
from (
	select 
		sp.supplier_id, 
		sp.supplier_product_id, 
		l.lookup_value as product_name, 
		l.lookup_type_id
	from
		reference.supplier_product sp
	inner join 
		reference.lookup l on sp.supplier_product_id = l.lookup_id
	where
		sp.supplier_id = _supplier_id
		and l.lookup_type_id = (select l.lookup_type_id from reference.lookup_type l where lookup_type_name='SupplierProduct')
		and now() <= coalesce(l.disabled_date, now())
	) a
	inner join 
			reference.lookup l on a.supplier_id = l.lookup_id;
end;
$$ language plpgsql;