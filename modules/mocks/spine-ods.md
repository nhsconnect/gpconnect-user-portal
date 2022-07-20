# Mock ODS Service

## Adding more data

- Change the data file for the `mock-ods` service in `docker-compose.yml`
  - **from** `spine-ods-fhir.json`
  - **to** `spine-ods-fhir.proxy.json`
- Record additional queries using the local proxy endpoint
  - e.g, http://localhost:5556/STU3/Organization/A6JR
  - Use a REST plugin or just the browser
- Go to the [Mountebank imposters console](http://localhost:2525/imposters)
- Open the mock and grab the extra data
  - Trim out any spurious bits you don't want like the `"_links"` key
- Add the extra data to the `"stubs"` key in the non-proxy file
- Swap the filename back in the `docker-compose.yml` file

## Data Included

The mocks currently include responses to the following queries

`/STU3/Organization?name=Berry`

`/STU3/Organization/A6JR`


```json
{
  "resourceType": "Organization",
  "id": "A6JR",
  "meta": {
    "lastUpdated": "2021-02-02T00:00:00+00:00",
    "profile": "https://fhir.nhs.uk/STU3/StructureDefinition/ODSAPI-Organization-1"
  },
  "extension": [
    {
      "url": "https://fhir.nhs.uk/STU3/StructureDefinition/Extension-ODSAPI-ActivePeriod-1",
      "valuePeriod": {
        "extension": [
          {
            "url": "https://fhir.nhs.uk/STU3/StructureDefinition/Extension-ODSAPI-DateType-1",
            "valueString": "Operational"
          }
        ],
        "start": "2008-11-25"
      }
    },
    {
      "url": "https://fhir.nhs.uk/STU3/StructureDefinition/Extension-ODSAPI-OrganizationRole-1",
      "extension": [
        {
          "url": "role",
          "valueCoding": {
            "system": "https://uat.directory.spineservices.nhs.uk/STU3/CodeSystem/ODSAPI-OrganizationRole-1",
            "code": "104",
            "display": "SOCIAL CARE PROVIDER"
          }
        },
        {
          "url": "primaryRole",
          "valueBoolean": true
        },
        {
          "url": "activePeriod",
          "valuePeriod": {
            "extension": [
              {
                "url": "https://fhir.nhs.uk/STU3/StructureDefinition/Extension-ODSAPI-DateType-1",
                "valueString": "Operational"
              }
            ],
            "start": "2008-11-25"
          }
        },
        {
          "url": "status",
          "valueString": "Active"
        }
      ]
    }
  ],
  "identifier": {
    "system": "https://fhir.nhs.uk/Id/ods-organization-code",
    "value": "A6JR"
  },
  "active": true,
  "type": {
    "coding": {
      "system": "https://fhir.nhs.uk/STU3/CodeSystem/ODSAPI-OrganizationRecordClass-1",
      "code": "1",
      "display": "HSCOrg"
    }
  },
  "name": "BERRYSTEAD NURSING & RESIDENTIAL HOME LTD",
  "address": {
    "line": [
      "1001 MELTON ROAD",
      "SYSTON"
    ],
    "city": "LEICESTER",
    "district": "LEICESTERSHIRE",
    "postalCode": "LE7 2BE",
    "country": "ENGLAND"
  }
}
```
