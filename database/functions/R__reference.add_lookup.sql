--
-- Name: add_lookup(integer, character varying, integer); Type: FUNCTION; Schema: reference; Owner: postgres
--

CREATE FUNCTION reference.add_lookup(
  _lookup_type_id integer,
  _lookup_value character varying,
  _linked_lookup_id integer DEFAULT NULL::integer
) RETURNS TABLE(
  lookup_id integer,
  lookup_type_id smallint,
  lookup_value character varying,
  linked_lookup_id integer,
  lookup_type_name character varying,
  lookup_type_description character varying,
  linked_lookup_value character varying,
  is_disabled boolean
)
    LANGUAGE plpgsql
    AS $$
declare	_lookup_id integer;
begin
	if not exists
	(
		select
			*
		from
			reference.lookup l
		where
			l.lookup_type_id = _lookup_type_id
			and l.lookup_value = _lookup_value
	)
	then
		insert into reference.lookup
		(
			lookup_value,
			lookup_type_id,
			added_date,
			linked_lookup_id
		)
		values
		(
			_lookup_value,
			_lookup_type_id,
			now(),
			_linked_lookup_id
		)
		returning
			reference.lookup.lookup_id
		into
			_lookup_id;
	else
		select into
			_lookup_id l.lookup_id
		from
			reference.lookup l
		where
			l.lookup_type_id = _lookup_type_id
			and l.lookup_value = _lookup_value;
	end if;

	return query
	select
		l.lookup_id,
		l.lookup_type_id,
		l.lookup_value,
		l.linked_lookup_id,
		lt.lookup_type_name,
		lt.lookup_type_description,
		l2.lookup_value,
		case when l.disabled_date is null then false else true end is_disabled
	from
		reference.lookup l
	left outer join reference.lookup l2 on l.linked_lookup_id = l2.lookup_id
	inner join reference.lookup_type lt on l.lookup_type_id = lt.lookup_type_id
	where
		l.lookup_id = _lookup_id;
end;
$$;


ALTER FUNCTION reference.add_lookup(
  _lookup_type_id integer,
  _lookup_value character varying,
  _linked_lookup_id integer
) OWNER TO postgres;

--
-- Name: FUNCTION add_lookup(_lookup_type_id integer, _lookup_value character varying, _linked_lookup_id integer); Type: ACL; Schema: reference; Owner: postgres
--

GRANT ALL ON FUNCTION reference.add_lookup(
  _lookup_type_id integer,
  _lookup_value character varying,
  _linked_lookup_id integer
) TO app_user;


