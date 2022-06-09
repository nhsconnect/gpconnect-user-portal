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
-- Data for Name: site_definition_status; Type: TABLE DATA; Schema: application; Owner: postgres
--

INSERT INTO application.site_definition_status (site_definition_status_id, site_definition_status_name) VALUES (1, 'Draft');
INSERT INTO application.site_definition_status (site_definition_status_id, site_definition_status_name) VALUES (2, 'Awaiting Review');
INSERT INTO application.site_definition_status (site_definition_status_id, site_definition_status_name) VALUES (3, 'Awaiting Spine Update');
INSERT INTO application.site_definition_status (site_definition_status_id, site_definition_status_name) VALUES (4, 'Completed');
INSERT INTO application.site_definition_status (site_definition_status_id, site_definition_status_name) VALUES (5, 'Live');

--
-- Data for Name: site_definition; Type: TABLE DATA; Schema: application; Owner: postgres
--

INSERT INTO application.site_definition (site_definition_id, site_ods_code, site_party_key, site_asid, site_unique_identifier, added_date, last_updated, site_definition_status_id, site_interactions, master_site_unique_identifier) VALUES (15646, 'X26', 'X26-822358', '200000001385', '011c9fb1-827b-4f0c-8fb3-72575a0108d7', '2022-03-03 12:05:45.648912', NULL, 5, 'urn:nhs:names:services:gpconnect:fhir:operation:gpc.getcarerecord_urn:nhs:names:services:gpconnect:fhir:rest:read:location_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata_urn:nhs:names:services:gpconnect:fhir:rest:read:metadata-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization_urn:nhs:names:services:gpconnect:fhir:rest:read:patient_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner_urn:nhs:names:services:gpconnect:fhir:rest:search:location_urn:nhs:names:services:gpconnect:fhir:rest:search:organization_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient_urn:nhs:names:services:gpconnect:fhir:rest:cancel:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:read:appointment-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1_urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1_urn:nhs:names:services:gpconnect:fhir:rest:update:appointment-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.registerpatient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:location-1_urn:nhs:names:services:gpconnect:fhir:rest:read:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:read:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:read:practitioner-1_urn:nhs:names:services:gpconnect:fhir:rest:search:organization-1_urn:nhs:names:services:gpconnect:fhir:rest:search:patient-1_urn:nhs:names:services:gpconnect:fhir:rest:search:practitioner-1_urn:nhs:names:services:gpconnect:fhir:operation:gpc.getstructuredrecord-1_urn:nhs:names:services:gpconnect:structured:fhir:rest:read:metadata-1', NULL);

--
-- Data for Name: site_attribute; Type: TABLE DATA; Schema: application; Owner: postgres
--

INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37323, 15646, 'SiteName', 'NHS DIGITAL', '2022-03-03 12:05:50.282075', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37324, 15646, 'SitePostcode', 'BA1 1DS', '2022-03-03 12:05:50.304313', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37325, 15646, 'SelectedCCGOdsCode', 'CCG Code', '2022-03-03 12:05:50.306734', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37326, 15646, 'SelectedCCGName', 'CCG Name', '2022-03-03 12:05:50.309085', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37327, 15646, 'OdsCode', 'X26', '2022-03-03 12:05:50.311536', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37328, 15646, 'SelectedSupplier', 'Trevors Software', '2022-03-03 12:05:50.313975', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37329, 15646, 'IsStructuredEnabled', 'True', '2022-03-03 12:05:50.316404', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37330, 15646, 'IsAppointmentEnabled', 'True', '2022-03-03 12:05:50.318891', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37331, 15646, 'IsHtmlEnabled', 'True', '2022-03-03 12:05:50.321189', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37332, 15646, 'IsSendDocumentEnabled', 'False', '2022-03-03 12:05:50.324059', NULL);
INSERT INTO application.site_attribute (site_attribute_id, site_definition_id, site_attribute_name, site_attribute_value, added_date, last_updated) VALUES (37333, 15646, 'UseCaseDescription', 'My Use Case', '2022-03-03 12:05:50.324059', NULL);
