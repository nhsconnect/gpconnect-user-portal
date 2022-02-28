drop function if exists application.get_site_attributes_friendly;

create function application.get_site_attributes_friendly
(
	_site_unique_identifier uuid
)
returns table
(
	"FieldName" varchar(100),
	"FieldValue" varchar(500)
)
as $$
begin
	return query
	select
		sa.site_attribute_name,
		COALESCE(l.lookup_value, sa.site_attribute_value)
	from 
		application.site_attribute sa
	inner join 
		application.site_definition sd on sa.site_definition_id = sd.site_definition_id
	left outer join
		reference.lookup l on sa.site_attribute_value = l.lookup_id::varchar
	where
		sd.site_unique_identifier = _site_unique_identifier
		and sa.site_attribute_value is not null
	order by
		sa.site_attribute_name;
end;
$$ language plpgsql;