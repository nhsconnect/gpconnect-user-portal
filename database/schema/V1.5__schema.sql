create table application.feedback
(
	feedback_id serial not null,
	overall_rating character varying(100) not null,
	improve_service text not null,
	added_date timestamp without time zone not null

	constraint application_feedback_improve_service_ck check (char_length(improve_service) > 0),
	constraint application_feedback_overall_rating_ck check (char_length(overall_rating) > 0)
);

GRANT SELECT,INSERT ON TABLE application.feedback TO app_user;
GRANT SELECT,UPDATE ON SEQUENCE application.feedback_feedback_id_seq TO app_user;