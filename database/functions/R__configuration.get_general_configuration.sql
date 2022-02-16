drop function if exists configuration.get_general_configuration;

create function configuration.get_general_configuration
(
)
returns table
(
	product_name varchar(100),
 	product_version varchar(100),
	admin_product_name varchar(100),
	get_access_email_address varchar(100)
)
as $$
begin
	return query
	select
		g.product_name,
		g.product_version,
		g.admin_product_name,
		g.get_access_email_address
	from configuration.general g;	
end;
$$ language plpgsql;