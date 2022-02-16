drop function if exists reference.get_supplier_product_capabilities;

create function reference.get_supplier_product_capabilities
(
	_supplier_product_id integer
)
returns table
(
	supplier_product_capability_id integer,
	supplier_product_id smallint,
	product_capability_id smallint,
	lookup_value varchar(500),
	supplier_id smallint,
	provider_assured boolean,
	consumer_assured boolean
)
as $$
begin	
	return query
	select 
		spc.supplier_product_capability_id,
		spc.supplier_product_id,
		spc.product_capability_id,
		l.lookup_value,
		sp.supplier_id,
		sp.provider_assured,
		sp.consumer_assured
	from
		reference.supplier_product_capability spc
		inner join reference.lookup l on spc.product_capability_id=l.lookup_id
		inner join reference.supplier_product sp on spc.supplier_product_id = sp.supplier_product_id
	where
		spc.supplier_product_id = _supplier_product_id
		and l.lookup_type_id = 4
		and now() <= coalesce(l.disabled_date, now());
end;
$$ language plpgsql;