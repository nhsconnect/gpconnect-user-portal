-- Rename app_user role to api
do $$
begin
	if exists (select * from pg_roles where rolname='app_user') then
		alter user app_user rename to api;
	end if;
end
$$;

-- Grant relevant permissions in application schema to API user
grant usage on schema application to api;
grant select, insert, update on all tables in schema application to api;
grant select, update on all sequences in schema application to api;
grant execute on all functions in schema application to api;

-- Grant relevant permissions in configuration schema to API user
grant usage on schema configuration to api;
grant select on all tables in schema configuration to api;
grant select, update on all sequences in schema configuration to api;
grant execute on all functions in schema configuration to api;

-- Grant relevant permissions in reference schema to API user
grant usage on schema reference to api;
grant select, insert, update on all tables in schema reference to api;
grant select, update on all sequences in schema reference to api;
grant execute on all functions in schema reference to api;

-- Grant rds_iam role to API user
do $$
begin
	if exists (select * from pg_roles where rolname='rds_iam') then
		grant rds_iam to api;
	end if;
end
$$;