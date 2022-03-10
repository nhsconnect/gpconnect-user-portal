drop function if exists reference.update_supplier_product_capabilities;

create function reference.update_supplier_product_capabilities
(
	_supplier_product_capability_id integer,
	_assurance_date timestamp without time zone,
	_awaiting_assurance boolean,
	_provider_assured boolean,
	_consumer_assured boolean,
	_capability_version varchar(100)
)
returns table
(
	supplier_product_capability_id integer,
	supplier_product_id smallint,
	product_capability_id smallint,
	lookup_value varchar(500),
	supplier_id smallint,
	provider_assured boolean,
	consumer_assured boolean,
	awaiting_assurance boolean,
	assurance_date timestamp without time zone,
	capability_version varchar(100)
)
as $$
begin	
	return query
	
	update
		reference.supplier_product_capability
	set
		provider_assured = _provider_assured, 
		consumer_assured = _consumer_assured, 
		awaiting_assurance = _awaiting_assurance, 
		assurance_date = _assurance_date, 
		capability_version = _capability_version
	where 
		reference.supplier_product_capability.supplier_product_capability_id = _supplier_product_capability_id;
	
	select 
		spc.supplier_product_capability_id,
		spc.supplier_product_id,
		spc.product_capability_id,
		l.lookup_value,
		sp.supplier_id,
		spc.provider_assured,
		spc.consumer_assured,
		spc.awaiting_assurance,
		spc.assurance_date,
		spc.capability_version
	from
		reference.supplier_product_capability spc
		inner join reference.lookup l on spc.product_capability_id=l.lookup_id
		inner join reference.supplier_product sp on spc.supplier_product_id = sp.supplier_product_id
	where
		spc.supplier_product_capability_id = _supplier_product_capability_id
		and l.lookup_type_id = 4
		and now() <= coalesce(l.disabled_date, now());
end;
$$ language plpgsql;