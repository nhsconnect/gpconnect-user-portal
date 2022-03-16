drop function if exists configuration.get_reference_configuration;

create function configuration.get_reference_configuration
(
)
returns table
(
	host_name varchar(100)
)
as $$
begin
	return query
	select
		r.host_name
	from configuration.reference r;	
end;
$$ language plpgsql;