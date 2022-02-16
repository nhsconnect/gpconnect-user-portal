drop function if exists reference.get_organisations;

create function reference.get_organisations
(
)
returns table
(
	organisation_id integer,
	organisation_name character varying(100),
	ods_code character varying(10),
	org_status character varying(50),
	org_record_class character varying(10),
	postcode character varying(10),
	last_change_date timestamp without time zone,
	primary_role_id character varying(10),
	primary_role_description character varying(100),
	organisation_link character varying(1000),
	added_date timestamp without time zone,
	last_sync_date timestamp without time zone
)
as $$
begin
	return query
	select
		o.organisation_id, 
		o.organisation_name, 
		o.ods_code, 
		o.org_status, 
		o.org_record_class, 
		o.postcode, 
		o.last_change_date, 
		o.primary_role_id, 
		o.primary_role_description, 
		o.organisation_link, 
		o.added_date, 
		o.last_sync_date
	from
		reference.organisation o
	order by 
		o.organisation_name;
end;
$$ language plpgsql;
