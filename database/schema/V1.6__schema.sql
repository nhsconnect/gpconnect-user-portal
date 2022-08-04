
DROP VIEW application.view_find_sites;

ALTER TABLE reference.lookup
	ALTER COLUMN lookup_value SET DATA TYPE text;

ALTER TABLE application.site_attribute
	ALTER COLUMN site_attribute_value SET DATA TYPE text;

CREATE VIEW application.view_find_sites AS
		select
			a.site_definition_id,
			a.site_ods_code,
			a.site_unique_identifier,
			a.site_interactions,
			a.sitename,
			a.selectedccgodscode,
			a.selectedccgname,
			a.isappointmentenabled,
			a.ishtmlenabled,
			a.isstructuredenabled,
			a.issenddocumentenabled,
			a.siteaddressline1,
			a.siteaddressline2,
			a.siteaddresstown,
			a.siteaddresscounty,
			a.siteaddresscountry,
			a.sitepostcode,
			a.odscode,
			a.selectedsupplier,
			a.usecasedescription
		from (
			select
				sd.site_definition_id,
				sd.site_ods_code,
				sd.site_unique_identifier,
				sd.site_interactions,
				json_object_agg(sa.site_attribute_name, sa.site_attribute_value) site_attributes_json,
				(json_populate_record(null::"application".site_attributes_type, json_object_agg(lower(sa.site_attribute_name), coalesce(l.lookup_value, sa.site_attribute_value)))).*
			from
				application.site_attribute sa
				inner join application.site_definition sd on sa.site_definition_id = sd.site_definition_id
				left outer join reference.lookup l on sa.site_attribute_value = l.lookup_id::varchar
			group by
				sd.site_definition_id
		) a;
