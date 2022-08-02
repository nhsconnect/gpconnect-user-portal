CREATE OR REPLACE FUNCTION application.add_feedback(_overall_rating character varying, _improve_service character varying)
RETURNS void
LANGUAGE plpgsql
AS $$
BEGIN
		insert into application.feedback
		(
			overall_rating,
			improve_service,
			added_date
		)
		values
		(
			_overall_rating,
			_improve_service,
			now()
		);		
END;
$$;

ALTER FUNCTION application.add_feedback(_overall_rating character varying,_improve_service character varying) OWNER TO postgres;

GRANT ALL ON FUNCTION application.add_feedback(_overall_rating character varying,_improve_service character varying) TO app_user;