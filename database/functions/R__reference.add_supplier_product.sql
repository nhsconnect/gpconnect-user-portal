drop function if exists reference.add_supplier_product;

create function reference.add_supplier_product
(
	_supplier_id integer,
	_supplier_product_id integer,
	_product_use_case character varying(1000)
)
returns void
as $$
begin
	if not exists
	(
		select 
			*
		from
			reference.supplier_product sp
		where
			sp.supplier_id = _supplier_id
			and sp.supplier_product_id = _supplier_product_id
	)
	then
		insert into reference.supplier_product 
		(
			supplier_id,
			supplier_product_id,
			product_use_case
		)
		values 
		(
			_supplier_id,
			_supplier_product_id,
			_product_use_case
		);
	end if;
end;
$$ language plpgsql;