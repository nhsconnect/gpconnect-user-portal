

CREATE ROLE app_user;


CREATE SCHEMA application;


ALTER SCHEMA application OWNER TO postgres;


CREATE SCHEMA reference;


ALTER SCHEMA reference OWNER TO postgres;


CREATE EXTENSION IF NOT EXISTS tablefunc WITH SCHEMA public;



COMMENT ON EXTENSION tablefunc IS 'functions that manipulate whole tables, including crosstab';



CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA application;



COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';



CREATE TYPE application.site_attributes_type AS (
	sitename character varying,
	selectedccgodscode character varying,
	selectedccgname character varying,
	isappointmentenabled boolean,
	ishtmlenabled boolean,
	isstructuredenabled boolean,
	issenddocumentenabled boolean,
	sitepostcode character varying,
	odscode character varying,
	selectedsupplier character varying,
	usecasedescription character varying
);


ALTER TYPE application.site_attributes_type OWNER TO postgres;


CREATE TYPE public.site_attributes_type AS (
	sitename character varying,
	selectedccgodscode character varying,
	selectedccgname character varying,
	isappointmentenabled boolean,
	ishtmlenabled boolean,
	isstructuredenabled boolean,
	issenddocumentenabled boolean,
	sitepostcode character varying,
	odscode character varying,
	selectedsupplier character varying
);


ALTER TYPE public.site_attributes_type OWNER TO postgres;


CREATE TABLE application.email_recipient (
    email_template_id smallint NOT NULL,
    email_address character varying(500),
    CONSTRAINT application_emailrecipient_emailaddress_ck CHECK ((char_length(btrim((email_address)::text)) > 0))
);


ALTER TABLE application.email_recipient OWNER TO postgres;


CREATE TABLE application.email_template (
    email_template_id smallint NOT NULL,
    subject character varying(100),
    body text,
    CONSTRAINT application_emailtemplate_body_ck CHECK ((char_length(btrim(body)) > 0)),
    CONSTRAINT application_emailtemplate_subject_ck CHECK ((char_length(btrim((subject)::text)) > 0))
);


ALTER TABLE application.email_template OWNER TO postgres;


CREATE TABLE application.site_attribute (
    site_attribute_id integer NOT NULL,
    site_definition_id integer NOT NULL,
    site_attribute_name character varying(100) NOT NULL,
    site_attribute_value character varying(500),
    added_date timestamp without time zone NOT NULL,
    last_updated timestamp without time zone
);


ALTER TABLE application.site_attribute OWNER TO postgres;


CREATE SEQUENCE application.site_attribute_site_attribute_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE application.site_attribute_site_attribute_id_seq OWNER TO postgres;


ALTER SEQUENCE application.site_attribute_site_attribute_id_seq OWNED BY application.site_attribute.site_attribute_id;



CREATE TABLE application.site_definition (
    site_definition_id integer NOT NULL,
    site_ods_code character varying(50),
    site_party_key character varying(50),
    site_asid character varying(50),
    site_unique_identifier uuid NOT NULL,
    added_date timestamp without time zone NOT NULL,
    last_updated timestamp without time zone,
    site_definition_status_id smallint NOT NULL,
    site_interactions character varying(4000),
    master_site_unique_identifier uuid
);


ALTER TABLE application.site_definition OWNER TO postgres;


CREATE SEQUENCE application.site_definition_site_definition_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE application.site_definition_site_definition_id_seq OWNER TO postgres;


ALTER SEQUENCE application.site_definition_site_definition_id_seq OWNED BY application.site_definition.site_definition_id;



CREATE TABLE application.site_definition_status (
    site_definition_status_id smallint NOT NULL,
    site_definition_status_name character varying(100),
    CONSTRAINT application_sitedefinitionstatus_sitedefinitionstatusname_ck CHECK ((char_length(btrim((site_definition_status_name)::text)) > 0))
);


ALTER TABLE application.site_definition_status OWNER TO postgres;


CREATE TABLE application."user" (
    user_id integer NOT NULL,
    email_address character varying(200) NOT NULL,
    last_logon_date timestamp without time zone,
    is_admin boolean DEFAULT false NOT NULL,
    added_date timestamp without time zone NOT NULL,
    authorised_date timestamp without time zone,
    CONSTRAINT application_user_emailaddress_ck CHECK ((char_length(btrim((email_address)::text)) > 0))
);


ALTER TABLE application."user" OWNER TO postgres;


CREATE TABLE application.user_session (
    user_session_id integer NOT NULL,
    user_id integer NOT NULL,
    start_time timestamp without time zone,
    end_time timestamp without time zone
);


ALTER TABLE application.user_session OWNER TO postgres;


CREATE SEQUENCE application.user_session_user_session_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE application.user_session_user_session_id_seq OWNER TO postgres;


ALTER SEQUENCE application.user_session_user_session_id_seq OWNED BY application.user_session.user_session_id;



CREATE SEQUENCE application.user_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE application.user_user_id_seq OWNER TO postgres;


ALTER SEQUENCE application.user_user_id_seq OWNED BY application."user".user_id;



CREATE TABLE reference.lookup (
    lookup_id integer NOT NULL,
    lookup_value character varying(500) NOT NULL,
    lookup_type_id smallint NOT NULL,
    added_date timestamp without time zone NOT NULL,
    disabled_date timestamp without time zone,
    linked_lookup_id integer,
    CONSTRAINT reference_lookup_lookupvalue_ck CHECK ((char_length(btrim((lookup_value)::text)) > 0))
);


ALTER TABLE reference.lookup OWNER TO postgres;


CREATE SEQUENCE reference.lookup_lookup_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE reference.lookup_lookup_id_seq OWNER TO postgres;


ALTER SEQUENCE reference.lookup_lookup_id_seq OWNED BY reference.lookup.lookup_id;



CREATE TABLE reference.lookup_type (
    lookup_type_id integer NOT NULL,
    lookup_type_name character varying(200) NOT NULL,
    lookup_type_description character varying(200) NOT NULL,
    is_system boolean DEFAULT false,
    CONSTRAINT reference_lookup_type_lookuptypename_ck CHECK ((char_length(btrim((lookup_type_name)::text)) > 0))
);


ALTER TABLE reference.lookup_type OWNER TO postgres;


CREATE SEQUENCE reference.lookup_type_lookup_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE reference.lookup_type_lookup_type_id_seq OWNER TO postgres;


ALTER SEQUENCE reference.lookup_type_lookup_type_id_seq OWNED BY reference.lookup_type.lookup_type_id;



CREATE TABLE reference.supplier_product (
    supplier_id smallint NOT NULL,
    supplier_product_id smallint NOT NULL,
    product_use_case character varying(1000)
);


ALTER TABLE reference.supplier_product OWNER TO postgres;


CREATE TABLE reference.supplier_product_capability (
    supplier_product_capability_id integer NOT NULL,
    supplier_product_id smallint NOT NULL,
    product_capability_id smallint NOT NULL,
    provider_assured boolean,
    consumer_assured boolean,
    awaiting_assurance boolean,
    assurance_date timestamp without time zone,
    capability_version character varying(50)
);


ALTER TABLE reference.supplier_product_capability OWNER TO postgres;


CREATE SEQUENCE reference.supplier_product_capability_supplier_product_capability_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE reference.supplier_product_capability_supplier_product_capability_id_seq OWNER TO postgres;


ALTER SEQUENCE reference.supplier_product_capability_supplier_product_capability_id_seq OWNED BY reference.supplier_product_capability.supplier_product_capability_id;



ALTER TABLE ONLY application.site_attribute ALTER COLUMN site_attribute_id SET DEFAULT nextval('application.site_attribute_site_attribute_id_seq'::regclass);



ALTER TABLE ONLY application.site_definition ALTER COLUMN site_definition_id SET DEFAULT nextval('application.site_definition_site_definition_id_seq'::regclass);



ALTER TABLE ONLY application."user" ALTER COLUMN user_id SET DEFAULT nextval('application.user_user_id_seq'::regclass);



ALTER TABLE ONLY application.user_session ALTER COLUMN user_session_id SET DEFAULT nextval('application.user_session_user_session_id_seq'::regclass);



ALTER TABLE ONLY reference.lookup ALTER COLUMN lookup_id SET DEFAULT nextval('reference.lookup_lookup_id_seq'::regclass);



ALTER TABLE ONLY reference.lookup_type ALTER COLUMN lookup_type_id SET DEFAULT nextval('reference.lookup_type_lookup_type_id_seq'::regclass);



ALTER TABLE ONLY reference.supplier_product_capability ALTER COLUMN supplier_product_capability_id SET DEFAULT nextval('reference.supplier_product_capability_supplier_product_capability_id_seq'::regclass);



COPY application.email_recipient (email_template_id, email_address) FROM stdin;
\.



COPY application.email_template (email_template_id, subject, body) FROM stdin;
\.



COPY application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) FROM stdin;
\.



COPY application.site_definition (site_definition_id, site_ods_code, site_party_key, site_asid, site_unique_identifier, added_date, last_updated, site_definition_status_id, site_interactions, master_site_unique_identifier) FROM stdin;
\.



COPY application.site_definition_status (site_definition_status_id, site_definition_status_name) FROM stdin;
\.



COPY application."user" (user_id, email_address, last_logon_date, is_admin, added_date, authorised_date) FROM stdin;
\.



COPY application.user_session (user_session_id, user_id, start_time, end_time) FROM stdin;
\.



COPY reference.lookup (lookup_id, lookup_value, lookup_type_id, added_date, disabled_date, linked_lookup_id) FROM stdin;
4	A&E	1	2021-12-17 09:16:58.167949	\N	\N
5	NHS 111	1	2021-12-17 09:16:58.167949	\N	\N
428	CENTRAL MIDLANDS COMMISSIONING HUB	6	2022-02-24 12:03:10.494018	\N	\N
429	14D	5	2022-02-24 12:03:10.494018	\N	428
430	CHESHIRE AND MERSEYSIDE COMMISSIONING HUB	6	2022-02-24 12:03:18.220649	\N	\N
1	Primary Care Network	1	2021-12-17 09:16:58.167949	\N	\N
3	General Practice	1	2021-12-17 09:16:58.167949	2022-03-02 13:50:01.044337	\N
7	Social Care	1	2021-12-17 09:16:58.167949	\N	\N
6	Extended Access Hub	1	2021-12-17 09:16:58.167949	\N	\N
8	Hub (other)	1	2021-12-17 09:16:58.167949	\N	\N
12	Pharmacy	1	2021-12-17 14:11:21.31547	\N	\N
14	Optometry	1	2021-12-17 14:11:36.353782	\N	\N
15	Shared Care Record	1	2021-12-17 14:11:43.080056	\N	\N
16	Secondary Care	1	2021-12-17 14:11:50.665743	\N	\N
17	Hospice	1	2021-12-17 14:11:58.946127	\N	\N
18	Online Consultation	1	2021-12-17 14:12:06.455078	\N	\N
19	Remote Consultation	1	2021-12-17 14:12:12.511942	\N	\N
20	Urgent Treatment Centre (UTC)	1	2021-12-17 14:12:19.690611	\N	\N
21	Minor Injuries Unit (MIU)	1	2021-12-17 14:12:28.4451	\N	\N
22	Walk In Centre	1	2021-12-17 14:12:38.231462	\N	\N
431	13Y	5	2022-02-24 12:03:18.220649	\N	430
432	CUMBRIA AND NORTH EAST COMMISSIONING HUB	6	2022-02-24 12:03:18.226094	\N	\N
433	13X	5	2022-02-24 12:03:18.226094	\N	432
434	EAST OF ENGLAND COMMISSIONING HUB	6	2022-02-24 12:03:18.229936	\N	\N
435	14E	5	2022-02-24 12:03:18.229936	\N	434
436	EAST OF ENGLAND - H&J COMMISSIONING HUB	6	2022-02-24 12:03:18.232398	\N	\N
437	14R	5	2022-02-24 12:03:18.232398	\N	436
66	HTML	4	2022-02-14 09:14:41.442603	\N	\N
67	Structured	4	2022-02-14 09:15:28.295779	\N	\N
68	Appointments	4	2022-02-14 09:15:28.295779	\N	\N
69	SendDocument	4	2022-02-15 16:24:10.47024	\N	\N
438	LANCASHIRE AND GREATER MANCHESTER COMMISSIONING HUB	6	2022-02-24 12:03:18.234712	\N	\N
439	13W	5	2022-02-24 12:03:18.234712	\N	438
440	LONDON COMMISSIONING HUB	6	2022-02-24 12:03:18.23698	\N	\N
441	13R	5	2022-02-24 12:03:18.23698	\N	440
442	LONDON - H&J COMMISSIONING HUB	6	2022-02-24 12:03:18.238749	\N	\N
443	14M	5	2022-02-24 12:03:18.238749	\N	442
444	MIDLANDS COMMISSIONING HUB	6	2022-02-24 12:03:18.240478	\N	\N
445	14A	5	2022-02-24 12:03:18.240478	\N	444
446	MIDLANDS - H&J COMMISSIONING HUB	6	2022-02-24 12:03:18.242425	\N	\N
447	14Q	5	2022-02-24 12:03:18.242425	\N	446
448	NATIONAL COMMISSIONING HUB 1	6	2022-02-24 12:03:18.244332	\N	\N
449	13Q	5	2022-02-24 12:03:18.244332	\N	448
450	NATIONAL COMMISSIONING HUB 2	6	2022-02-24 12:03:18.246152	\N	\N
451	15L	5	2022-02-24 12:03:18.246152	\N	450
452	NHS BARKING AND DAGENHAM CCG	6	2022-02-24 12:03:18.247818	\N	\N
453	07L	5	2022-02-24 12:03:18.247818	\N	452
454	NHS BARNSLEY CCG	6	2022-02-24 12:03:18.249481	\N	\N
455	02P	5	2022-02-24 12:03:18.249481	\N	454
456	NHS BASILDON AND BRENTWOOD CCG	6	2022-02-24 12:03:18.251266	\N	\N
457	99E	5	2022-02-24 12:03:18.251266	\N	456
458	NHS BASSETLAW CCG	6	2022-02-24 12:03:18.252957	\N	\N
459	02Q	5	2022-02-24 12:03:18.252957	\N	458
460	NHS BATH AND NORTH EAST SOMERSET, SWINDON AND WILTSHIRE CCG	6	2022-02-24 12:03:18.25464	\N	\N
461	92G	5	2022-02-24 12:03:18.25464	\N	460
462	NHS BEDFORDSHIRE CCG	6	2022-02-24 12:03:18.25633	\N	\N
463	06F	5	2022-02-24 12:03:18.25633	\N	462
464	NHS BEDFORDSHIRE, LUTON AND MILTON KEYNES CCG	6	2022-02-24 12:03:18.258085	\N	\N
465	M1J4Y	5	2022-02-24 12:03:18.258085	\N	464
466	NHS BERKSHIRE WEST CCG	6	2022-02-24 12:03:18.260536	\N	\N
467	15A	5	2022-02-24 12:03:18.260536	\N	466
468	NHS BIRMINGHAM AND SOLIHULL CCG	6	2022-02-24 12:03:18.262748	\N	\N
469	15E	5	2022-02-24 12:03:18.262748	\N	468
470	NHS BIRMINGHAM CROSSCITY CCG	6	2022-02-24 12:03:18.265124	\N	\N
471	13P	5	2022-02-24 12:03:18.265124	\N	470
472	NHS BIRMINGHAM SOUTH AND CENTRAL CCG	6	2022-02-24 12:03:18.267543	\N	\N
473	04X	5	2022-02-24 12:03:18.267543	\N	472
474	NHS BLACKBURN WITH DARWEN CCG	6	2022-02-24 12:03:18.269642	\N	\N
475	00Q	5	2022-02-24 12:03:18.269642	\N	474
476	NHS BLACK COUNTRY AND WEST BIRMINGHAM CCG	6	2022-02-24 12:03:18.271516	\N	\N
477	D2P2L	5	2022-02-24 12:03:18.271516	\N	476
478	NHS BLACKPOOL CCG	6	2022-02-24 12:03:18.273294	\N	\N
479	00R	5	2022-02-24 12:03:18.273294	\N	478
480	NHS BOLTON CCG	6	2022-02-24 12:03:18.275159	\N	\N
481	00T	5	2022-02-24 12:03:18.275159	\N	480
482	NHS BRADFORD DISTRICT AND CRAVEN CCG	6	2022-02-24 12:03:18.277069	\N	\N
483	36J	5	2022-02-24 12:03:18.277069	\N	482
484	NHS BRENT CCG	6	2022-02-24 12:03:18.278773	\N	\N
485	07P	5	2022-02-24 12:03:18.278773	\N	484
486	NHS BRIGHTON AND HOVE CCG	6	2022-02-24 12:03:18.280554	\N	\N
487	09D	5	2022-02-24 12:03:18.280554	\N	486
488	NHS BRISTOL CCG	6	2022-02-24 12:03:18.282509	\N	\N
489	11H	5	2022-02-24 12:03:18.282509	\N	488
490	NHS BRISTOL, NORTH SOMERSET AND SOUTH GLOUCESTERSHIRE CCG	6	2022-02-24 12:03:18.284393	\N	\N
491	15C	5	2022-02-24 12:03:18.284393	\N	490
2	Community Provider	1	2021-12-17 09:16:58.167949	2022-03-02 13:41:10.424179	\N
492	NHS BUCKINGHAMSHIRE CCG	6	2022-02-24 12:03:18.293976	\N	\N
493	14Y	5	2022-02-24 12:03:18.293976	\N	492
496	NHS CALDERDALE CCG	6	2022-02-24 12:03:18.299871	\N	\N
497	02T	5	2022-02-24 12:03:18.299871	\N	496
500	NHS CANNOCK CHASE CCG	6	2022-02-24 12:03:18.303891	\N	\N
501	04Y	5	2022-02-24 12:03:18.303891	\N	500
504	NHS CENTRAL LONDON (WESTMINSTER) CCG	6	2022-02-24 12:03:18.307372	\N	\N
505	09A	5	2022-02-24 12:03:18.307372	\N	504
508	NHS CHESHIRE CCG	6	2022-02-24 12:03:18.310902	\N	\N
509	27D	5	2022-02-24 12:03:18.310902	\N	508
512	NHS CITY AND HACKNEY CCG	6	2022-02-24 12:03:18.314959	\N	\N
513	07T	5	2022-02-24 12:03:18.314959	\N	512
516	NHS COVENTRY AND RUGBY CCG	6	2022-02-24 12:03:18.318216	\N	\N
517	05A	5	2022-02-24 12:03:18.318216	\N	516
520	NHS DERBY AND DERBYSHIRE CCG	6	2022-02-24 12:03:18.321514	\N	\N
521	15M	5	2022-02-24 12:03:18.321514	\N	520
524	NHS DONCASTER CCG	6	2022-02-24 12:03:18.324939	\N	\N
525	02X	5	2022-02-24 12:03:18.324939	\N	524
528	NHS DUDLEY CCG	6	2022-02-24 12:03:18.328984	\N	\N
529	05C	5	2022-02-24 12:03:18.328984	\N	528
532	NHS EAST AND NORTH HERTFORDSHIRE CCG	6	2022-02-24 12:03:18.333671	\N	\N
533	06K	5	2022-02-24 12:03:18.333671	\N	532
536	NHS EAST LANCASHIRE CCG	6	2022-02-24 12:03:18.337754	\N	\N
537	01A	5	2022-02-24 12:03:18.337754	\N	536
540	NHS EAST RIDING OF YORKSHIRE CCG	6	2022-02-24 12:03:18.341122	\N	\N
541	02Y	5	2022-02-24 12:03:18.341122	\N	540
544	NHS EAST SUSSEX CCG	6	2022-02-24 12:03:18.344692	\N	\N
545	97R	5	2022-02-24 12:03:18.344692	\N	544
548	NHS FRIMLEY CCG	6	2022-02-24 12:03:18.349151	\N	\N
549	D4U1Y	5	2022-02-24 12:03:18.349151	\N	548
552	NHS GLOUCESTERSHIRE CCG	6	2022-02-24 12:03:18.352597	\N	\N
553	11M	5	2022-02-24 12:03:18.352597	\N	552
556	NHS GREATER PRESTON CCG	6	2022-02-24 12:03:18.356016	\N	\N
557	01E	5	2022-02-24 12:03:18.356016	\N	556
560	NHS HAMMERSMITH AND FULHAM CCG	6	2022-02-24 12:03:18.359625	\N	\N
561	08C	5	2022-02-24 12:03:18.359625	\N	560
564	NHS HARROW CCG	6	2022-02-24 12:03:18.364122	\N	\N
565	08E	5	2022-02-24 12:03:18.364122	\N	564
568	NHS HEREFORDSHIRE AND WORCESTERSHIRE CCG	6	2022-02-24 12:03:18.368675	\N	\N
569	18C	5	2022-02-24 12:03:18.368675	\N	568
572	NHS HEYWOOD, MIDDLETON AND ROCHDALE CCG	6	2022-02-24 12:03:18.372363	\N	\N
573	01D	5	2022-02-24 12:03:18.372363	\N	572
576	NHS HOUNSLOW CCG	6	2022-02-24 12:03:18.376058	\N	\N
577	07Y	5	2022-02-24 12:03:18.376058	\N	576
580	NHS IPSWICH AND EAST SUFFOLK CCG	6	2022-02-24 12:03:18.379967	\N	\N
581	06L	5	2022-02-24 12:03:18.379967	\N	580
584	NHS KENT AND MEDWAY CCG	6	2022-02-24 12:03:18.383322	\N	\N
585	91Q	5	2022-02-24 12:03:18.383322	\N	584
588	NHS KIRKLEES CCG	6	2022-02-24 12:03:18.386902	\N	\N
589	X2C4Y	5	2022-02-24 12:03:18.386902	\N	588
592	NHS LEEDS CCG	6	2022-02-24 12:03:18.390184	\N	\N
593	15F	5	2022-02-24 12:03:18.390184	\N	592
596	NHS LEICESTER CITY CCG	6	2022-02-24 12:03:18.394137	\N	\N
597	04C	5	2022-02-24 12:03:18.394137	\N	596
600	NHS LIVERPOOL CCG	6	2022-02-24 12:03:18.398595	\N	\N
601	99A	5	2022-02-24 12:03:18.398595	\N	600
604	NHS MANCHESTER CCG	6	2022-02-24 12:03:18.402785	\N	\N
605	14L	5	2022-02-24 12:03:18.402785	\N	604
608	NHS MILTON KEYNES CCG	6	2022-02-24 12:03:18.406422	\N	\N
609	04F	5	2022-02-24 12:03:18.406422	\N	608
612	NHS NEWCASTLE GATESHEAD CCG	6	2022-02-24 12:03:18.41025	\N	\N
613	13T	5	2022-02-24 12:03:18.41025	\N	612
616	NHS NORFOLK AND WAVENEY CCG	6	2022-02-24 12:03:18.413681	\N	\N
617	26A	5	2022-02-24 12:03:18.413681	\N	616
620	NHS NORTH CENTRAL LONDON CCG	6	2022-02-24 12:03:18.416983	\N	\N
621	93C	5	2022-02-24 12:03:18.416983	\N	620
624	NHS NORTH EAST ESSEX CCG	6	2022-02-24 12:03:18.420255	\N	\N
625	06T	5	2022-02-24 12:03:18.420255	\N	624
628	NHS NORTH EAST LINCOLNSHIRE CCG	6	2022-02-24 12:03:18.423618	\N	\N
629	03H	5	2022-02-24 12:03:18.423618	\N	628
632	NHS NORTH HAMPSHIRE CCG	6	2022-02-24 12:03:18.428178	\N	\N
633	10J	5	2022-02-24 12:03:18.428178	\N	632
636	NHS NORTH LINCOLNSHIRE CCG	6	2022-02-24 12:03:18.432633	\N	\N
637	03K	5	2022-02-24 12:03:18.432633	\N	636
640	NHS NORTH TYNESIDE CCG	6	2022-02-24 12:03:18.436978	\N	\N
641	99C	5	2022-02-24 12:03:18.436978	\N	640
644	NHS NORTH WEST LONDON CCG	6	2022-02-24 12:03:18.440909	\N	\N
645	W2U3Z	5	2022-02-24 12:03:18.440909	\N	644
648	NHS NOTTINGHAM AND NOTTINGHAMSHIRE CCG	6	2022-02-24 12:03:18.445914	\N	\N
649	52R	5	2022-02-24 12:03:18.445914	\N	648
652	NHS OXFORDSHIRE CCG	6	2022-02-24 12:03:18.450996	\N	\N
653	10Q	5	2022-02-24 12:03:18.450996	\N	652
656	NHS REDBRIDGE CCG	6	2022-02-24 12:03:18.456584	\N	\N
657	08N	5	2022-02-24 12:03:18.456584	\N	656
660	NHS SALFORD CCG	6	2022-02-24 12:03:18.462503	\N	\N
661	01G	5	2022-02-24 12:03:18.462503	\N	660
664	NHS SHEFFIELD CCG	6	2022-02-24 12:03:18.468114	\N	\N
665	03N	5	2022-02-24 12:03:18.468114	\N	664
668	NHS SHROPSHIRE, TELFORD AND WREKIN CCG	6	2022-02-24 12:03:18.480832	\N	\N
669	M2L0M	5	2022-02-24 12:03:18.480832	\N	668
672	NHS SOMERSET CCG	6	2022-02-24 12:03:18.486394	\N	\N
673	11X	5	2022-02-24 12:03:18.486394	\N	672
676	NHS SOUTH EASTERN HAMPSHIRE CCG	6	2022-02-24 12:03:18.490856	\N	\N
677	10V	5	2022-02-24 12:03:18.490856	\N	676
680	NHS SOUTH EAST STAFFORDSHIRE AND SEISDON PENINSULA CCG	6	2022-02-24 12:03:18.49555	\N	\N
681	05Q	5	2022-02-24 12:03:18.49555	\N	680
684	NHS SOUTH GLOUCESTERSHIRE CCG	6	2022-02-24 12:03:18.50082	\N	\N
685	12A	5	2022-02-24 12:03:18.50082	\N	684
688	NHS SOUTH SEFTON CCG	6	2022-02-24 12:03:18.506363	\N	\N
689	01T	5	2022-02-24 12:03:18.506363	\N	688
692	NHS SOUTH WARWICKSHIRE CCG	6	2022-02-24 12:03:18.511137	\N	\N
693	05R	5	2022-02-24 12:03:18.511137	\N	692
696	NHS STAFFORD AND SURROUNDS CCG	6	2022-02-24 12:03:18.515394	\N	\N
697	05V	5	2022-02-24 12:03:18.515394	\N	696
700	NHS STOCKPORT CCG	6	2022-02-24 12:03:18.520087	\N	\N
701	01W	5	2022-02-24 12:03:18.520087	\N	700
704	NHS SUNDERLAND CCG	6	2022-02-24 12:03:18.524046	\N	\N
705	00P	5	2022-02-24 12:03:18.524046	\N	704
708	NHS SURREY HEATH CCG	6	2022-02-24 12:03:18.528929	\N	\N
709	10C	5	2022-02-24 12:03:18.528929	\N	708
712	NHS TEES VALLEY CCG	6	2022-02-24 12:03:18.534504	\N	\N
713	16C	5	2022-02-24 12:03:18.534504	\N	712
716	NHS THURROCK CCG	6	2022-02-24 12:03:18.538851	\N	\N
717	07G	5	2022-02-24 12:03:18.538851	\N	716
720	NHS TRAFFORD CCG	6	2022-02-24 12:03:18.542802	\N	\N
721	02A	5	2022-02-24 12:03:18.542802	\N	720
724	NHS WAKEFIELD CCG	6	2022-02-24 12:03:18.546872	\N	\N
725	03R	5	2022-02-24 12:03:18.546872	\N	724
728	NHS WALTHAM FOREST CCG	6	2022-02-24 12:03:18.550649	\N	\N
729	08W	5	2022-02-24 12:03:18.550649	\N	728
732	NHS WARWICKSHIRE NORTH CCG	6	2022-02-24 12:03:18.554142	\N	\N
733	05H	5	2022-02-24 12:03:18.554142	\N	732
736	NHS WEST HAMPSHIRE CCG	6	2022-02-24 12:03:18.557617	\N	\N
737	11A	5	2022-02-24 12:03:18.557617	\N	736
740	NHS WEST LEICESTERSHIRE CCG	6	2022-02-24 12:03:18.561841	\N	\N
494	NHS BURY CCG	6	2022-02-24 12:03:18.296838	\N	\N
495	00V	5	2022-02-24 12:03:18.296838	\N	494
498	NHS CAMBRIDGESHIRE AND PETERBOROUGH CCG	6	2022-02-24 12:03:18.302069	\N	\N
499	06H	5	2022-02-24 12:03:18.302069	\N	498
502	NHS CASTLE POINT AND ROCHFORD CCG	6	2022-02-24 12:03:18.305714	\N	\N
503	99F	5	2022-02-24 12:03:18.305714	\N	502
506	NHS CENTRAL MANCHESTER CCG	6	2022-02-24 12:03:18.309194	\N	\N
507	00W	5	2022-02-24 12:03:18.309194	\N	506
510	NHS CHORLEY AND SOUTH RIBBLE CCG	6	2022-02-24 12:03:18.312744	\N	\N
511	00X	5	2022-02-24 12:03:18.312744	\N	510
514	NHS COUNTY DURHAM CCG	6	2022-02-24 12:03:18.316567	\N	\N
515	84H	5	2022-02-24 12:03:18.316567	\N	514
518	NHS COVENTRY AND WARWICKSHIRE CCG	6	2022-02-24 12:03:18.319882	\N	\N
519	B2M3M	5	2022-02-24 12:03:18.319882	\N	518
522	NHS DEVON CCG	6	2022-02-24 12:03:18.323116	\N	\N
523	15N	5	2022-02-24 12:03:18.323116	\N	522
526	NHS DORSET CCG	6	2022-02-24 12:03:18.326821	\N	\N
527	11J	5	2022-02-24 12:03:18.326821	\N	526
530	NHS EALING CCG	6	2022-02-24 12:03:18.331392	\N	\N
531	07W	5	2022-02-24 12:03:18.331392	\N	530
534	NHS EAST BERKSHIRE CCG	6	2022-02-24 12:03:18.335855	\N	\N
535	15D	5	2022-02-24 12:03:18.335855	\N	534
538	NHS EAST LEICESTERSHIRE AND RUTLAND CCG	6	2022-02-24 12:03:18.339418	\N	\N
539	03W	5	2022-02-24 12:03:18.339418	\N	538
542	NHS EAST STAFFORDSHIRE CCG	6	2022-02-24 12:03:18.342933	\N	\N
543	05D	5	2022-02-24 12:03:18.342933	\N	542
546	NHS FAREHAM AND GOSPORT CCG	6	2022-02-24 12:03:18.346994	\N	\N
547	10K	5	2022-02-24 12:03:18.346994	\N	546
550	NHS FYLDE AND WYRE CCG	6	2022-02-24 12:03:18.35092	\N	\N
551	02M	5	2022-02-24 12:03:18.35092	\N	550
554	NHS GREATER HUDDERSFIELD CCG	6	2022-02-24 12:03:18.354285	\N	\N
555	03A	5	2022-02-24 12:03:18.354285	\N	554
558	NHS HALTON CCG	6	2022-02-24 12:03:18.357737	\N	\N
559	01F	5	2022-02-24 12:03:18.357737	\N	558
562	NHS HAMPSHIRE, SOUTHAMPTON AND ISLE OF WIGHT CCG	6	2022-02-24 12:03:18.361895	\N	\N
563	D9Y0V	5	2022-02-24 12:03:18.361895	\N	562
566	NHS HAVERING CCG	6	2022-02-24 12:03:18.366461	\N	\N
567	08F	5	2022-02-24 12:03:18.366461	\N	566
570	NHS HERTS VALLEYS CCG	6	2022-02-24 12:03:18.370702	\N	\N
571	06N	5	2022-02-24 12:03:18.370702	\N	570
574	NHS HILLINGDON CCG	6	2022-02-24 12:03:18.37399	\N	\N
575	08G	5	2022-02-24 12:03:18.37399	\N	574
578	NHS HULL CCG	6	2022-02-24 12:03:18.37825	\N	\N
579	03F	5	2022-02-24 12:03:18.37825	\N	578
582	NHS ISLE OF WIGHT CCG	6	2022-02-24 12:03:18.381608	\N	\N
583	10L	5	2022-02-24 12:03:18.381608	\N	582
586	NHS KERNOW CCG	6	2022-02-24 12:03:18.385151	\N	\N
587	11N	5	2022-02-24 12:03:18.385151	\N	586
590	NHS KNOWSLEY CCG	6	2022-02-24 12:03:18.388575	\N	\N
591	01J	5	2022-02-24 12:03:18.388575	\N	590
594	NHS LEEDS WEST CCG	6	2022-02-24 12:03:18.391979	\N	\N
595	03C	5	2022-02-24 12:03:18.391979	\N	594
598	NHS LINCOLNSHIRE CCG	6	2022-02-24 12:03:18.396393	\N	\N
599	71E	5	2022-02-24 12:03:18.396393	\N	598
602	NHS LUTON CCG	6	2022-02-24 12:03:18.400722	\N	\N
603	06P	5	2022-02-24 12:03:18.400722	\N	602
606	NHS MID ESSEX CCG	6	2022-02-24 12:03:18.404581	\N	\N
607	06Q	5	2022-02-24 12:03:18.404581	\N	606
610	NHS MORECAMBE BAY CCG	6	2022-02-24 12:03:18.408257	\N	\N
611	01K	5	2022-02-24 12:03:18.408257	\N	610
614	NHS NEWHAM CCG	6	2022-02-24 12:03:18.411941	\N	\N
615	08M	5	2022-02-24 12:03:18.411941	\N	614
618	NHS NORTHAMPTONSHIRE CCG	6	2022-02-24 12:03:18.415389	\N	\N
619	78H	5	2022-02-24 12:03:18.415389	\N	618
622	NHS NORTH CUMBRIA CCG	6	2022-02-24 12:03:18.418657	\N	\N
623	01H	5	2022-02-24 12:03:18.418657	\N	622
626	NHS NORTH EAST HAMPSHIRE AND FARNHAM CCG	6	2022-02-24 12:03:18.421918	\N	\N
627	99M	5	2022-02-24 12:03:18.421918	\N	626
630	NHS NORTH EAST LONDON CCG	6	2022-02-24 12:03:18.426007	\N	\N
631	A3A8R	5	2022-02-24 12:03:18.426007	\N	630
634	NHS NORTH KIRKLEES CCG	6	2022-02-24 12:03:18.430396	\N	\N
635	03J	5	2022-02-24 12:03:18.430396	\N	634
638	NHS NORTH STAFFORDSHIRE CCG	6	2022-02-24 12:03:18.434826	\N	\N
639	05G	5	2022-02-24 12:03:18.434826	\N	638
642	NHS NORTHUMBERLAND CCG	6	2022-02-24 12:03:18.438693	\N	\N
643	00L	5	2022-02-24 12:03:18.438693	\N	642
646	NHS NORTH YORKSHIRE CCG	6	2022-02-24 12:03:18.443506	\N	\N
647	42D	5	2022-02-24 12:03:18.443506	\N	646
650	NHS OLDHAM CCG	6	2022-02-24 12:03:18.448304	\N	\N
651	00Y	5	2022-02-24 12:03:18.448304	\N	650
654	NHS PORTSMOUTH CCG	6	2022-02-24 12:03:18.454002	\N	\N
655	10R	5	2022-02-24 12:03:18.454002	\N	654
658	NHS ROTHERHAM CCG	6	2022-02-24 12:03:18.459496	\N	\N
659	03L	5	2022-02-24 12:03:18.459496	\N	658
662	NHS SANDWELL AND WEST BIRMINGHAM CCG	6	2022-02-24 12:03:18.465358	\N	\N
663	05L	5	2022-02-24 12:03:18.465358	\N	662
666	NHS SHROPSHIRE CCG	6	2022-02-24 12:03:18.47089	\N	\N
667	05N	5	2022-02-24 12:03:18.47089	\N	666
670	NHS SOLIHULL CCG	6	2022-02-24 12:03:18.48374	\N	\N
671	05P	5	2022-02-24 12:03:18.48374	\N	670
674	NHS SOUTHAMPTON CCG	6	2022-02-24 12:03:18.48884	\N	\N
675	10X	5	2022-02-24 12:03:18.48884	\N	674
678	NHS SOUTH EAST LONDON CCG	6	2022-02-24 12:03:18.493189	\N	\N
679	72Q	5	2022-02-24 12:03:18.493189	\N	678
682	NHS SOUTHEND CCG	6	2022-02-24 12:03:18.498051	\N	\N
683	99G	5	2022-02-24 12:03:18.498051	\N	682
686	NHS SOUTHPORT AND FORMBY CCG	6	2022-02-24 12:03:18.50378	\N	\N
687	01V	5	2022-02-24 12:03:18.50378	\N	686
690	NHS SOUTH TYNESIDE CCG	6	2022-02-24 12:03:18.508792	\N	\N
691	00N	5	2022-02-24 12:03:18.508792	\N	690
694	NHS SOUTH WEST LONDON CCG	6	2022-02-24 12:03:18.513204	\N	\N
695	36L	5	2022-02-24 12:03:18.513204	\N	694
698	NHS ST HELENS CCG	6	2022-02-24 12:03:18.517915	\N	\N
699	01X	5	2022-02-24 12:03:18.517915	\N	698
702	NHS STOKE ON TRENT CCG	6	2022-02-24 12:03:18.522131	\N	\N
703	05W	5	2022-02-24 12:03:18.522131	\N	702
706	NHS SURREY HEARTLANDS CCG	6	2022-02-24 12:03:18.526229	\N	\N
707	92A	5	2022-02-24 12:03:18.526229	\N	706
710	NHS TAMESIDE AND GLOSSOP CCG	6	2022-02-24 12:03:18.531427	\N	\N
711	01Y	5	2022-02-24 12:03:18.531427	\N	710
714	NHS TELFORD AND WREKIN CCG	6	2022-02-24 12:03:18.536957	\N	\N
715	05X	5	2022-02-24 12:03:18.536957	\N	714
718	NHS TOWER HAMLETS CCG	6	2022-02-24 12:03:18.540994	\N	\N
719	08V	5	2022-02-24 12:03:18.540994	\N	718
722	NHS VALE OF YORK CCG	6	2022-02-24 12:03:18.544725	\N	\N
723	03Q	5	2022-02-24 12:03:18.544725	\N	722
726	NHS WALSALL CCG	6	2022-02-24 12:03:18.548738	\N	\N
727	05Y	5	2022-02-24 12:03:18.548738	\N	726
730	NHS WARRINGTON CCG	6	2022-02-24 12:03:18.552395	\N	\N
731	02E	5	2022-02-24 12:03:18.552395	\N	730
734	NHS WEST ESSEX CCG	6	2022-02-24 12:03:18.55594	\N	\N
735	07H	5	2022-02-24 12:03:18.55594	\N	734
738	NHS WEST LANCASHIRE CCG	6	2022-02-24 12:03:18.559527	\N	\N
739	02G	5	2022-02-24 12:03:18.559527	\N	738
742	NHS WEST LONDON CCG	6	2022-02-24 12:03:18.568919	\N	\N
743	08Y	5	2022-02-24 12:03:18.568919	\N	742
746	NHS WEST SUSSEX CCG	6	2022-02-24 12:03:18.597169	\N	\N
741	04V	5	2022-02-24 12:03:18.561841	\N	740
744	NHS WEST SUFFOLK CCG	6	2022-02-24 12:03:18.581113	\N	\N
745	07K	5	2022-02-24 12:03:18.581113	\N	744
748	NHS WIGAN BOROUGH CCG	6	2022-02-24 12:03:18.620567	\N	\N
749	02H	5	2022-02-24 12:03:18.620567	\N	748
752	NHS WIRRAL CCG	6	2022-02-24 12:03:18.744194	\N	\N
753	12F	5	2022-02-24 12:03:18.744194	\N	752
756	NHS WOLVERHAMPTON CCG	6	2022-02-24 12:03:18.806653	\N	\N
757	06A	5	2022-02-24 12:03:18.806653	\N	756
760	NORTH EAST AND YORKSHIRE - H&J COMMISSIONING HUB	6	2022-02-24 12:03:18.811233	\N	\N
761	76A	5	2022-02-24 12:03:18.811233	\N	760
764	NORTH WEST - H&J COMMISSIONING HUB	6	2022-02-24 12:03:18.818821	\N	\N
765	32T	5	2022-02-24 12:03:18.818821	\N	764
768	SOUTH EAST COMMISSIONING HUB	6	2022-02-24 12:03:19.255796	\N	\N
769	14G	5	2022-02-24 12:03:19.255796	\N	768
772	SOUTH WEST COMMISSIONING HUB	6	2022-02-24 12:03:19.558919	\N	\N
773	14F	5	2022-02-24 12:03:19.558919	\N	772
776	SOUTH WEST NORTH COMMISSIONING HUB	6	2022-02-24 12:03:19.563352	\N	\N
777	15H	5	2022-02-24 12:03:19.563352	\N	776
780	YORKSHIRE AND HUMBER COMMISSIONING HUB	6	2022-02-24 12:03:19.686536	\N	\N
781	13V	5	2022-02-24 12:03:19.686536	\N	780
747	70F	5	2022-02-24 12:03:18.597169	\N	746
750	NHS WINDSOR, ASCOT AND MAIDENHEAD CCG	6	2022-02-24 12:03:18.659465	\N	\N
751	11C	5	2022-02-24 12:03:18.659465	\N	750
754	NHS WOKINGHAM CCG	6	2022-02-24 12:03:18.790855	\N	\N
755	11D	5	2022-02-24 12:03:18.790855	\N	754
758	NORTH EAST AND YORKSHIRE COMMISSIONING HUB	6	2022-02-24 12:03:18.809019	\N	\N
759	85J	5	2022-02-24 12:03:18.809019	\N	758
762	NORTH WEST COMMISSIONING HUB	6	2022-02-24 12:03:18.81438	\N	\N
763	27T	5	2022-02-24 12:03:18.81438	\N	762
766	SOUTH CENTRAL COMMISSIONING HUB	6	2022-02-24 12:03:19.253646	\N	\N
767	14H	5	2022-02-24 12:03:19.253646	\N	766
770	SOUTH EAST - H&J COMMISSIONING HUB	6	2022-02-24 12:03:19.55661	\N	\N
771	97T	5	2022-02-24 12:03:19.55661	\N	770
774	SOUTH WEST - H&J COMMISSIONING HUB	6	2022-02-24 12:03:19.561078	\N	\N
775	14T	5	2022-02-24 12:03:19.561078	\N	774
778	WEST MIDLANDS COMMISSIONING HUB	6	2022-02-24 12:03:19.684497	\N	\N
779	14C	5	2022-02-24 12:03:19.684497	\N	778
13	Dental Practice 2222	1	2021-12-17 14:11:30.98875	2022-03-02 12:44:27.018204	\N
785	Demonstrative	1	2022-03-16 10:37:02.256528	\N	\N
786	refvrferfre	1	2022-03-16 10:40:11.735749	\N	\N
787	AAAAAAAAAAAAAAA	1	2022-03-16 10:45:21.658889	\N	\N
788	JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ	1	2022-03-16 10:54:57.162941	\N	\N
23	Advanced	2	2022-05-09 15:58:46.023379	\N	\N
24	Appt Health (MW)	2	2022-05-09 15:58:46.023379	\N	\N
26	Cambridge	2	2022-05-09 15:58:46.023379	\N	\N
27	Care Banking	2	2022-05-09 15:58:46.023379	\N	\N
28	Cleric	2	2022-05-09 15:58:46.023379	\N	\N
29	Devon	2	2022-05-09 15:58:46.023379	\N	\N
33	Eva	2	2022-05-09 15:58:46.023379	\N	\N
34	Everylife Technologies	2	2022-05-09 15:58:46.023379	\N	\N
36	IC24	2	2022-05-09 15:58:46.023379	\N	\N
37	Intersystems	2	2022-05-09 15:58:46.023379	\N	\N
38	Leeds Teaching Hospital	2	2022-05-09 15:58:46.023379	\N	\N
39	Meddbase	2	2022-05-09 15:58:46.023379	\N	\N
40	Medloop	2	2022-05-09 15:58:46.023379	\N	\N
41	Medusa	2	2022-05-09 15:58:46.023379	\N	\N
42	MIS-ES	2	2022-05-09 15:58:46.023379	\N	\N
43	Nervecentre	2	2022-05-09 15:58:46.023379	\N	\N
44	North Lincs & Goole	2	2022-05-09 15:58:46.023379	\N	\N
45	Nourish	2	2022-05-09 15:58:46.023379	\N	\N
46	Person Centred Software (PCS)	2	2022-05-09 15:58:46.023379	\N	\N
47	Plymouth	2	2022-05-09 15:58:46.023379	\N	\N
48	Push Dr	2	2022-05-09 15:58:46.023379	\N	\N
49	Restart	2	2022-05-09 15:58:46.023379	\N	\N
50	Servelec	2	2022-05-09 15:58:46.023379	\N	\N
51	Sussex LHCR	2	2022-05-09 15:58:46.023379	\N	\N
52	Sussex Partnership Foundation Trust (SPFT)	2	2022-05-09 15:58:46.023379	\N	\N
54	Humber (Upstream/YHCR)	2	2022-05-09 15:58:46.023379	\N	\N
55	Yorkshire & Humber	2	2022-05-09 15:58:46.023379	\N	\N
56	Vision	2	2022-05-09 15:58:46.023379	\N	\N
64	Product Name 3	3	2022-05-09 15:58:46.023379	\N	\N
\.



COPY reference.lookup_type (lookup_type_id, lookup_type_name, lookup_type_description, is_system) FROM stdin;
1	CareSetting	Care Setting	f
2	Supplier	Supplier	f
3	SupplierProduct	Supplier Product	t
4	Capability	Supplier Product Capability	t
5	CCGICBODSCode	CCG/ICB ODS Code	t
6	CCGICBName	CCG/ICB Name	t
\.



COPY reference.supplier_product (supplier_id, supplier_product_id, product_use_case) FROM stdin;
\.



COPY reference.supplier_product_capability (supplier_product_capability_id, supplier_product_id, product_capability_id, provider_assured, consumer_assured, awaiting_assurance, assurance_date, capability_version) FROM stdin;
\.



SELECT pg_catalog.setval('application.site_attribute_site_attribute_id_seq', 1, false);



SELECT pg_catalog.setval('application.site_definition_site_definition_id_seq', 1, false);



SELECT pg_catalog.setval('application.user_session_user_session_id_seq', 1, false);



SELECT pg_catalog.setval('application.user_user_id_seq', 1, false);



SELECT pg_catalog.setval('reference.lookup_lookup_id_seq', 788, true);



SELECT pg_catalog.setval('reference.lookup_type_lookup_type_id_seq', 1, true);



SELECT pg_catalog.setval('reference.supplier_product_capability_supplier_product_capability_id_seq', 17, true);



ALTER TABLE ONLY application.email_template
    ADD CONSTRAINT application_emailtemplate_emailtemplateid_pk PRIMARY KEY (email_template_id);



ALTER TABLE ONLY application.site_attribute
    ADD CONSTRAINT application_siteattribute_siteattributeid_pk PRIMARY KEY (site_attribute_id);



ALTER TABLE ONLY application.site_definition
    ADD CONSTRAINT application_sitedefinition_sitedefinitionid_pk PRIMARY KEY (site_definition_id);



ALTER TABLE ONLY application.site_definition_status
    ADD CONSTRAINT application_sitedefinitionstatus_sitedefinitionstatusid_pk PRIMARY KEY (site_definition_status_id);



ALTER TABLE ONLY application."user"
    ADD CONSTRAINT application_user_userid_pk PRIMARY KEY (user_id);



ALTER TABLE ONLY reference.lookup
    ADD CONSTRAINT reference_lookup_lookupid_pk PRIMARY KEY (lookup_id);



ALTER TABLE ONLY reference.lookup_type
    ADD CONSTRAINT reference_lookup_type_lookuptypeid_pk PRIMARY KEY (lookup_type_id);



ALTER TABLE ONLY reference.supplier_product_capability
    ADD CONSTRAINT reference_supplierproductcapability_productcapabilityid_pk PRIMARY KEY (supplier_product_capability_id);



CREATE UNIQUE INDEX application_siteattribute_siteattributename_ix ON application.site_attribute USING btree (site_definition_id, site_attribute_name);



CREATE UNIQUE INDEX application_sitedefinition_siteuniqueidentifier_ix ON application.site_definition USING btree (site_unique_identifier);



CREATE UNIQUE INDEX application_user_emailaddress_ix ON application."user" USING btree (lower((email_address)::text));



CREATE UNIQUE INDEX reference_lookuptype_lookuptypename_ix ON reference.lookup_type USING btree (lower((lookup_type_name)::text));



ALTER TABLE ONLY application.email_recipient
    ADD CONSTRAINT application_emailtemplate_emailtemplateid_fk FOREIGN KEY (email_template_id) REFERENCES application.email_template(email_template_id);



ALTER TABLE ONLY application.site_attribute
    ADD CONSTRAINT application_siteattribute_sitedefinitionid_fk FOREIGN KEY (site_definition_id) REFERENCES application.site_definition(site_definition_id);



ALTER TABLE ONLY application.site_definition
    ADD CONSTRAINT application_sitedefinition_sitedefinitionstatusid_fk FOREIGN KEY (site_definition_status_id) REFERENCES application.site_definition_status(site_definition_status_id);



ALTER TABLE ONLY reference.lookup
    ADD CONSTRAINT reference_lookup_lookuptypeid_fk FOREIGN KEY (lookup_type_id) REFERENCES reference.lookup_type(lookup_type_id);



ALTER TABLE ONLY reference.supplier_product
    ADD CONSTRAINT reference_supplierproduct_supplierid_fk FOREIGN KEY (supplier_id) REFERENCES reference.lookup(lookup_id);



ALTER TABLE ONLY reference.supplier_product
    ADD CONSTRAINT reference_supplierproduct_supplierproductid_fk FOREIGN KEY (supplier_product_id) REFERENCES reference.lookup(lookup_id);



ALTER TABLE ONLY reference.supplier_product_capability
    ADD CONSTRAINT reference_supplierproductcapability_productcapabilityid_fk FOREIGN KEY (product_capability_id) REFERENCES reference.lookup(lookup_id);



ALTER TABLE ONLY reference.supplier_product_capability
    ADD CONSTRAINT reference_supplierproductcapability_supplierproductid_fk FOREIGN KEY (supplier_product_id) REFERENCES reference.lookup(lookup_id);



GRANT USAGE ON SCHEMA application TO app_user;



REVOKE ALL ON SCHEMA public FROM PUBLIC;



GRANT USAGE ON SCHEMA reference TO app_user;



GRANT ALL ON TYPE application.site_attributes_type TO app_user;



GRANT ALL ON TYPE public.site_attributes_type TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.email_recipient TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.email_template TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.site_attribute TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE application.site_attribute_site_attribute_id_seq TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.site_definition TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE application.site_definition_site_definition_id_seq TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.site_definition_status TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application."user" TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE application.user_session TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE application.user_session_user_session_id_seq TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE application.user_user_id_seq TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE reference.lookup TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE reference.lookup_lookup_id_seq TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE reference.lookup_type TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE reference.lookup_type_lookup_type_id_seq TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE reference.supplier_product TO app_user;



GRANT SELECT,INSERT,UPDATE ON TABLE reference.supplier_product_capability TO app_user;



GRANT SELECT,UPDATE ON SEQUENCE reference.supplier_product_capability_supplier_product_capability_id_seq TO app_user;



