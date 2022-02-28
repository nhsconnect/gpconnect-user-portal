drop function if exists application.set_user_isadmin;

create function application.set_user_isadmin
(
	_admin_user_id integer,
	_user_id integer,
	_is_admin boolean
)
returns void
as $$
begin
	update
		application.user
	set
		is_admin = _is_admin,
		authorised_date = case when _is_admin = true then now() else null end
	where 
		user_id = _user_id;
end;
$$ language plpgsql;