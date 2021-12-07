drop function if exists reference.synchronise_organisation;

create function reference.synchronise_organisation
(
	_ods_code varchar(10),
	_organisation_name varchar(100),
	_org_status varchar(50),
	_org_record_class varchar(10),
	_last_change_date timestamp,
	_primary_role_id varchar(10),
	_primary_role_description varchar(100),
	_organisation_link varchar(1000),
	_postcode varchar(10)
)
returns void
as $$
begin	
	if not exists
	(
		select 
			*
		from reference.organisation o
		where o.ods_code = _ods_code
	)
	then
		insert into reference.organisation
		(
			organisation_name, 
			ods_code, 
			org_status, 
			org_record_class,
			postcode,
			last_change_date,
			primary_role_id,
			primary_role_description,
			organisation_link,
			added_date,
			last_sync_date
		)
		values
		(
			_organisation_name,
			_ods_code,
			_org_status,
			_org_record_class,
			_postcode,
			_last_change_date,
			_primary_role_id,
			_primary_role_description,
			_organisation_link,
			now(),
			now()
		);
	else
		update application.organisation
		set
			organisation_name = _organisation_name, 
			ods_code = _ods_code, 
			org_status = _org_status, 
			org_record_class = _org_record_class,
			postcode = _postcode,
			last_change_date = _last_change_date,
			primary_role_id = _primary_role_id,
			primary_role_description = _primary_role_description,
			organisation_link = _organisation_link,
			last_sync_date = now()
		where ods_code = _ods_code;
	end if;
end;
$$ language plpgsql;