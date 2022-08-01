--
-- Name: get_supplier_product_capabilities(integer, boolean); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.get_supplier_product_capabilities(
  _supplier_product_id integer,
  _include_supplier_product_capabilities_not_enabled boolean
) RETURNS TABLE(
  supplier_product_capability_id integer,
  supplier_product_id smallint,
  product_capability_id smallint,
  lookup_value text,
  supplier_id smallint,
  provider_assured boolean,
  consumer_assured boolean,
  awaiting_assurance boolean,
  assurance_date timestamp without time zone,
  capability_version character varying,
  can_send_action_request boolean
)
    LANGUAGE plpgsql
    AS $$
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
		lookup_value text,
		supplier_id smallint,
		provider_assured boolean,
		consumer_assured boolean,
		awaiting_assurance boolean,
		assurance_date timestamp without time zone,
		capability_version character varying(50),
		can_send_action_request boolean
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
		capability_version,
		can_send_action_request
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
		spc.capability_version,
		(spc.provider_assured OR spc.consumer_assured) AND ((spc.assurance_date is not null AND spc.assurance_date <= now()) OR spc.awaiting_assurance) can_send_action_request
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
			capability_version,
			can_send_action_request
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
			null,
			false
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
		espc.capability_version,
		espc.can_send_action_request
	from
		enabled_supplier_product_capabilities espc;
end;
$$;


ALTER FUNCTION reference.get_supplier_product_capabilities(
  _supplier_product_id integer,
  _include_supplier_product_capabilities_not_enabled boolean
) OWNER TO postgres;

