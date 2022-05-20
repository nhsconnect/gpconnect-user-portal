--
-- Name: add_supplier_product(integer, integer, character varying); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.add_supplier_product(
  _supplier_id integer,
  _supplier_product_id integer,
  _product_use_case character varying
) RETURNS void
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION reference.add_supplier_product(
  _supplier_id integer,
  _supplier_product_id integer,
  _product_use_case character varying
) OWNER TO postgres;

--
-- Name: FUNCTION add_supplier_product(_supplier_id integer, _supplier_product_id integer, _product_use_case character varying); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.add_supplier_product(
  _supplier_id integer,
  _supplier_product_id integer,
  _product_use_case character varying
) TO app_user;


