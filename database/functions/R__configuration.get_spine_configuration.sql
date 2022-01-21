drop function if exists configuration.get_spine_configuration;

create function configuration.get_spine_configuration
(
)
returns table
(
    spine_fhir_api_directory_services_fqdn varchar(100),
    spine_fhir_api_systems_register_fqdn varchar(100),
    spine_fhir_api_key varchar(100)
)
as $$
begin
	return query
	select 
		s.spine_fhir_api_directory_services_fqdn, 
		s.spine_fhir_api_systems_register_fqdn, 
		s.spine_fhir_api_key	    
	from
		configuration.spine s;
end;
$$ language plpgsql;