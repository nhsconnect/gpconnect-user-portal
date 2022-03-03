drop function if exists application.add_site_definition;

create function application.add_site_definition
(
	_site_unique_identifier uuid,
	_site_ods_code varchar(50),
	_site_party_key varchar(50),
	_site_asid varchar(50),
	_site_definition_status smallint,
	_site_interactions varchar(4000) default null,
	_master_site_unique_identifier uuid default null
)
returns table
(
	site_ods_code varchar(50),
	site_party_key varchar(50),
	site_asid varchar(50),
	site_unique_identifier uuid,
	site_definition_id integer,
	site_definition_status_id smallint,
	site_interactions varchar(4000)
)
as $$
declare	_site_definition_id integer;
begin
	if not exists
	(
		select 
			*
		from
			application.site_definition sd
		where
			sd.site_ods_code = _site_ods_code
			and sd.site_party_key = _site_party_key
			and sd.site_asid = _site_asid
			and	sd.site_definition_status_id = _site_definition_status
	)
	then
		insert into application.site_definition
		(
			site_ods_code, 
			site_party_key,
			site_asid, 
			site_unique_identifier,
			added_date,
			site_definition_status_id,
			site_interactions,
			master_site_unique_identifier
		)
		values
		(
			_site_ods_code,
			_site_party_key,
			_site_asid,
			_site_unique_identifier,
			now(),
			_site_definition_status,
			_site_interactions,
			_master_site_unique_identifier
		)
		returning
			application.site_definition.site_definition_id
		into 
			_site_definition_id;
	else
		if 
		(
			_site_ods_code = '' and 
			_site_party_key = '' and 
			_site_asid = ''
		)
		then
			insert into application.site_definition
			(
				site_ods_code, 
				site_party_key,
				site_asid, 
				site_unique_identifier,
				added_date,
				site_definition_status_id,
				master_site_unique_identifier
			)
			values
			(
				_site_ods_code,
				_site_party_key,
				_site_asid,
				_site_unique_identifier,
				now(),
				_site_definition_status,
				_master_site_unique_identifier
			)
			returning
				application.site_definition.site_definition_id
			into 
				_site_definition_id;
		else
			select into 
				_site_definition_id sd.site_definition_id
			from 
				application.site_definition sd
			where
				sd.site_ods_code = _site_ods_code
				and sd.site_party_key = _site_party_key
				and sd.site_asid = _site_asid
				and sd.site_definition_status_id = _site_definition_status;
		end if;
	end if;
		
	return query
	select
		sd.site_ods_code, 
		sd.site_party_key,
		sd.site_asid, 
		sd.site_unique_identifier,
		sd.site_definition_id,
		sd.site_definition_status_id,
		sd.site_interactions
	from 
		application.site_definition sd
	where
		sd.site_definition_id = _site_definition_id;
end;
$$ language plpgsql;