drop function if exists application.get_endpoint_change_count_by_status;

create function application.get_endpoint_change_count_by_status
(	
)
returns table
(
	site_definition_status_id smallint,
	site_definition_status_name varchar(100),
	site_definition_status_count bigint
)
as $$
begin
	return query
		select			
			sd.site_definition_status_id,
			sds.site_definition_status_name,
			count(*) site_definition_status_count		
		from 
			application.site_definition sd
			inner join application.site_definition_status sds on sd.site_definition_status_id = sds.site_definition_status_id
		group by
			sd.site_definition_status_id,
			sds.site_definition_status_name;
end;
$$ language plpgsql;