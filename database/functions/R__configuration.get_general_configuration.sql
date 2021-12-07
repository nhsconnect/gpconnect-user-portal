drop function if exists configuration.get_general_configuration;

create function configuration.get_general_configuration
(
)
returns table
(
	product_name varchar(100),
 	product_version varchar(100)
)
as $$
begin
	return query
	select
		g.product_name,
		g.product_version
	from configuration.general g;	
end;
$$ language plpgsql;