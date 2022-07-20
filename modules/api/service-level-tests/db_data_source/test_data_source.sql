--
-- PostgreSQL database dump
--

-- Dumped from database version 11.9
-- Dumped by pg_dump version 13.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: site_definition; Type: TABLE DATA; Schema: application; Owner: postgres
--

INSERT INTO application.site_definition (site_definition_id, site_ods_code, site_party_key, site_asid, site_unique_identifier, added_date, last_updated, site_definition_status_id, site_interactions, master_site_unique_identifier) VALUES (15646, 'X26', 'X26-822358', '200000001385', '011c9fb1-827b-4f0c-8fb3-72575a0108d7', '2022-03-03 12:05:45.648912', NULL, 5, 'urn:nhs:names:services:gpconnect:fhir:operation:gpc.getcarerecord_urn:nhs:names:services:gpconnect:fhir:rest:read:location_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization_urn:nhs:names:services:gpconnect:fhir:rest:read:patient_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner_urn:nhs:names:services:gpconnect:fhir:rest:search:location_urn:nhs:names:services:gpconnect:fhir:rest:search:organization_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient_urn:nhs:names:services:gpconnect:fhir:rest:cancel:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:read:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1_urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1_urn:nhs:names:services:gpconnect:fhir:rest:update:appointment-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:location-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:read:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner-1_urn:nhs:names:services:gpconnect:fhir:rest:search:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.getstructuredrecord-1_urn:nhs:names:services:gpconnect:structured:fhir:rest:read:metadata-1', NULL);
--
-- Data for Name: site_attribute; Type: TABLE DATA; Schema: application; Owner: postgres
--
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43188, 15646, 'SiteName', 'NHS DIGITAL', '2022-06-16 13:01:39.757299', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43189, 15646, 'SiteAddressLine1', 'THE LEEDS GOVERNMENT HUB', '2022-06-16 13:01:59.675763', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43190, 15646, 'SiteAddressLine2', '7-8 WELLINGTON PLACE', '2022-06-16 13:01:59.755114', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43191, 15646, 'SiteAddressTown', 'LEEDS', '2022-06-16 13:01:59.761869', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43192, 15646, 'SiteAddressCounty', 'TestCounty', '2022-06-16 13:01:59.767858', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43193, 15646, 'SiteAddressCountry', 'ENGLAND', '2022-06-16 13:01:59.770226', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43194, 15646, 'SitePostcode', 'LS1 4AP', '2022-06-16 13:01:59.772732', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43195, 15646, 'SelectedCCGOdsCode', '05P', '2022-06-16 13:01:59.775112', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43196, 15646, 'SelectedCCGName', 'NHS SOLIHULL CCG', '2022-06-16 13:01:59.777574', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43197, 15646, 'OdsCode', 'X26', '2022-06-16 13:01:59.779971', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43198, 15646, 'IsStructuredEnabled', 'True', '2022-06-16 13:01:59.782225', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43199, 15646, 'IsAppointmentEnabled', 'True', '2022-06-16 13:01:59.784363', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43200, 15646, 'IsHtmlEnabled', 'True', '2022-06-16 13:01:59.786406', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43201, 15646, 'IsSendDocumentEnabled', 'False', '2022-06-16 13:01:59.788611', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37333, 15646, 'UseCaseDescription', 'My Use Case', '2022-03-03 12:05:50.324059', NULL);

--
-- Data for Name: site_definition; Type: TABLE DATA; Schema: application; Owner: postgres
--

INSERT INTO application.site_definition (site_definition_id, site_ods_code, site_party_key, site_asid, site_unique_identifier, added_date, last_updated, site_definition_status_id, site_interactions, master_site_unique_identifier) VALUES (15647, 'X27', 'X27-822358', '200000001386', '011c9fb1-827b-4f0c-8fb3-72575a0108d8', '2022-03-03 12:06:45.648912', NULL, 5, 'urn:nhs:names:services:gpconnect:fhir:operation:gpc.getcarerecord_urn:nhs:names:services:gpconnect:fhir:rest:read:location_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization_urn:nhs:names:services:gpconnect:fhir:rest:read:patient_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner_urn:nhs:names:services:gpconnect:fhir:rest:search:location_urn:nhs:names:services:gpconnect:fhir:rest:search:organization_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient_urn:nhs:names:services:gpconnect:fhir:rest:cancel:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:read:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1_urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1_urn:nhs:names:services:gpconnect:fhir:rest:update:appointment-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:location-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:read:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner-1_urn:nhs:names:services:gpconnect:fhir:rest:search:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.getstructuredrecord-1_urn:nhs:names:services:gpconnect:structured:fhir:rest:read:metadata-1', NULL);
--
-- Data for Name: site_attribute; Type: TABLE DATA; Schema: application; Owner: postgres
--
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43203, 15647, 'SiteName', 'NHS VEGETAL', '2022-06-16 13:01:39.757299', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43204, 15647, 'SiteAddressLine1', 'THE LEEKS GOVERNMENT HUB', '2022-06-16 13:01:59.675763', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43205, 15647, 'SiteAddressLine2', '7-8 BEEF WELLINGTON PLACE', '2022-06-16 13:01:59.755114', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43206, 15647, 'SiteAddressTown', 'LEEKS', '2022-06-16 13:01:59.761869', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43207, 15647, 'SiteAddressCounty', 'Yorkshire Puddingshire', '2022-06-16 13:01:59.767858', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43208, 15647, 'SiteAddressCountry', 'ENGLAND', '2022-06-16 13:01:59.770226', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43209, 15647, 'SitePostcode', 'LS2 5AP', '2022-06-16 13:01:59.772732', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43210, 15647, 'SelectedCCGOdsCode', '06P', '2022-06-16 13:01:59.775112', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43211, 15647, 'SelectedCCGName', 'NHS CELERIAC CCG', '2022-06-16 13:01:59.777574', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43212, 15647, 'OdsCode', 'X27', '2022-06-16 13:01:59.779971', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43213, 15647, 'IsStructuredEnabled', 'True', '2022-06-16 13:01:59.782225', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43214, 15647, 'IsAppointmentEnabled', 'False', '2022-06-16 13:01:59.784363', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43215, 15647, 'IsHtmlEnabled', 'True', '2022-06-16 13:01:59.786406', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (43216, 15647, 'IsSendDocumentEnabled', 'True', '2022-06-16 13:01:59.788611', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37348, 15647, 'Testing Stuff', 'Testing Stuff', '2022-03-03 12:05:50.324059', NULL);
