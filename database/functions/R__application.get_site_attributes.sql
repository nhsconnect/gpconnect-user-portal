drop function if exists application.get_site_attributes;

create function application.get_site_attributes
(
	_site_unique_identifier uuid
)
returns table
(
	site_attribute_id integer,
	site_definition_id integer,
	site_attribute_name varchar(100),
	site_attribute_value varchar(500),
	site_unique_identifier uuid
)
as $$
begin
	return query
	select
		sa.site_attribute_id,
		sa.site_definition_id,
		sa.site_attribute_name,
		sa.site_attribute_value,
		sd.site_unique_identifier
	from 
		application.site_attribute sa
	inner join 
		application.site_definition sd on sa.site_definition_id = sd.site_definition_id
	where
		sd.site_unique_identifier = _site_unique_identifier;
end;
$$ language plpgsql;