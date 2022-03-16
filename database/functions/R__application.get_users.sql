drop function if exists application.get_users;

create function application.get_users
(
)
returns table
(
	user_id integer,
	email_address varchar(200),
	last_logon_date timestamp without time zone,
	is_admin boolean,
	added_date timestamp without time zone,
	authorised_date timestamp without time zone
)
as $$
begin
	return query
	select
		u.user_id, 
		u.email_address,		
		u.last_logon_date,
		u.is_admin,
		u.added_date,
		u.authorised_date
	from
		application.user u				
	order by
		u.email_address;
end;
$$ language plpgsql;