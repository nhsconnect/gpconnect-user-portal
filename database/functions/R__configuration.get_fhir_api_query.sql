drop function if exists configuration.get_fhir_api_query;

create function configuration.get_fhir_api_query
(
	_fhir_api_query_type varchar(50)
)
returns table
(
	query_name varchar(100),
	query_text varchar(1000)
)
as $$
begin
	return query
	select
		q.query_name,
		q.query_text
	from configuration.fhir_api_query q	
	where q.query_name = _fhir_api_query_type;
end;
$$ language plpgsql;