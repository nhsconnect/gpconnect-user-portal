drop function if exists application.add_site_attribute;

create function application.add_site_attribute
(
	_site_unique_identifier uuid,
	_site_attribute_name varchar(100),
	_site_attribute_value varchar(500)
)
returns table
(
	site_unique_identifier uuid,
	site_attribute_id integer,
	site_attribute_name varchar(100),
	site_attribute_value varchar(500),
	site_definition_id integer
)
as $$
declare
	_site_definition_id integer;
begin
	select into 
		_site_definition_id sd.site_definition_id
	from 
		application.site_definition sd
	where
		sd.site_unique_identifier = _site_unique_identifier;		
		
	if not exists
	(
		select 
			*
		from
			application.site_attribute sa
		where
			sa.site_definition_id = _site_definition_id
			and sa.site_attribute_name = _site_attribute_name
	)
	then
		insert into application.site_attribute
		(
			site_attribute_name,
			site_attribute_value,
			site_definition_id,
			added_date
		)
		values
		(
			_site_attribute_name,
			_site_attribute_value,
			_site_definition_id,
			now()
		);
	else
		update 
			application.site_attribute
		set
			site_attribute_value = _site_attribute_value,
			last_updated = now()
		where
			site_definition_id = _site_definition_id
			and site_attribute_name = _site_attribute_name;
	end if;
	
	return query
	select
		sd.site_unique_identifier,
		sa.site_attribute_id,
		sa.site_attribute_name,
		sa.site_attribute_value,
		sa.site_definition_id
	from 
		application.site_attribute sa
		inner join application.site_definition sd on sa.site_definition_id = sd.site_definition_id
	where
		sa.site_definition_id = _site_definition_id
		and sa.site_attribute_name = _site_attribute_name;		
end;
$$ language plpgsql;