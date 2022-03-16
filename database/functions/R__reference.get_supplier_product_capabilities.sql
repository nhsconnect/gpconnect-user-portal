drop function if exists reference.get_supplier_product_capabilities;

create function reference.get_supplier_product_capabilities
(
	_supplier_product_id integer,
	_include_supplier_product_capabilities_not_enabled boolean
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
declare _supplier_id smallint;
begin	
	select 
		sp.supplier_id into _supplier_id	
	from
		reference.supplier_product_capability spc
		inner join reference.lookup l on spc.product_capability_id=l.lookup_id
		inner join reference.supplier_product sp on spc.supplier_product_id = sp.supplier_product_id
	where
		spc.supplier_product_id = _supplier_product_id
		and l.lookup_type_id = 4;

	drop table if exists enabled_supplier_product_capabilities;
	
	create temp table enabled_supplier_product_capabilities
	(
		supplier_product_capability_id integer,
		supplier_product_id smallint,
		product_capability_id smallint,
		lookup_value character varying(500),
		supplier_id smallint,
		provider_assured boolean,
		consumer_assured boolean,
		awaiting_assurance boolean,
		assurance_date timestamp without time zone,
		capability_version character varying(50)
	);
	
	insert into enabled_supplier_product_capabilities 
	(
		supplier_product_capability_id,
		supplier_product_id,
		product_capability_id,
		lookup_value,
		supplier_id,
		provider_assured,
		consumer_assured,
		awaiting_assurance,
		assurance_date,
		capability_version
	)	
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
		spc.supplier_product_id = _supplier_product_id
		and l.lookup_type_id = 4
		and now() <= coalesce(l.disabled_date, now());
	
	if _include_supplier_product_capabilities_not_enabled then
	
		insert into enabled_supplier_product_capabilities 
		(
			supplier_product_capability_id,
			supplier_product_id,
			product_capability_id,
			lookup_value,
			supplier_id,
			provider_assured,
			consumer_assured,
			awaiting_assurance,
			assurance_date,
			capability_version
		)	
		select 
			null,
			_supplier_product_id,
			l.lookup_id,
			l.lookup_value,
			_supplier_id,
			null,
			null,
			null,
			null,
			null
		from
			reference.lookup l
		where
			l.lookup_id not in 
			(
				select
					espc.product_capability_id 
				from
					enabled_supplier_product_capabilities espc
			)
			and lookup_type_id = 4;
	end if;
	
	return query
	select
		espc.supplier_product_capability_id,
		espc.supplier_product_id,
		espc.product_capability_id,
		espc.lookup_value,
		espc.supplier_id,
		espc.provider_assured,
		espc.consumer_assured,
		espc.awaiting_assurance,
		espc.assurance_date,
		espc.capability_version
	from
		enabled_supplier_product_capabilities espc;
end;
$$ language plpgsql;