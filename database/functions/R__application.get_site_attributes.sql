drop function if exists application.get_site_attributes;

create function application.get_site_attributes
(
	_site_unique_identifier uuid
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
	return query
	select
		sa.site_attribute_id,
		sa.site_definition_id,
		sd.site_unique_identifier,
		sa.site_attribute_name,
		sa.site_attribute_value site_attribute_value,
		l.lookup_value lookup_value	
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