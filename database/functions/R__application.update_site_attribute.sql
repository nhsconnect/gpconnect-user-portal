drop function if exists application.update_site_attribute;

create function application.update_site_attribute
(
	_site_unique_identifier uuid,
	_site_attribute_name varchar(100),
	_site_attribute_value varchar(500)
)
returns table
(
	site_attribute_id integer,
	site_definition_id integer,
	site_unique_identifier uuid,
	site_attribute_name varchar(100),
	site_attribute_value varchar(500),
	lookup_value varchar(500)
)
as $$
begin
	update
		application.site_attribute sa
	set
		site_attribute_value = _site_attribute_value,
		last_updated = now()
	from
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier
		and sa.site_definition_id = sd.site_definition_id
		and sa.site_attribute_name = _site_attribute_name;
		
	return query
	select
		sa.site_attribute_id,
		sa.site_definition_id,
		sd.site_unique_identifier,
		sa.site_attribute_name,
		sa.site_attribute_value,
		l.lookup_value
	from 
		application.site_attribute sa
	inner join 
		application.site_definition sd on sa.site_definition_id = sd.site_definition_id
	left outer join
		reference.lookup l on sa.site_attribute_value = l.lookup_id::varchar
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;