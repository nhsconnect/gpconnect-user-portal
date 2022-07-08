var supertest = require('supertest');
const { Client } = require('pg');

const agreement = require('../models/agreement');

var ENDPOINT = process.env.ENDPOINT;

const TEST_ODS_CODE = 'MyTestODSCode1';

describe('Agreement Endpoint', () => {
    const request = supertest(ENDPOINT);

    describe('Returns appropriate errors', () => {
        it ('returns 400 when a blank request is sent', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send({})
            .expect(400, done);
        })
        it ('returns 400 when odsCode is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({odsCode: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation name is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({name: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation addressLine1 is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({addressLine1: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation town is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({town: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation county is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({county: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation country is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({country: null}))
            .expect(400, done);
        })
        it ('returns 400 when organisation postcode is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({postcode: null}))
            .expect(400, done);
        })
        it ('returns 400 when usecase is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({ useCase: null}))
            .expect(400, done);
        })
        it ('returns 400 when signatory name is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({ signatoryName: null}))
            .expect(400, done);
        })
        it ('returns 400 when signatory email is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({ signatoryEmail: null}))
            .expect(400, done);
        })
        it ('returns 400 when signatory email is missing', (done, err) => {
            request
            .post('/agreement')
            .set('Content-type', 'application/json')
            .send(agreement({ signatoryPosition: null}))
            .expect(400, done);
        })
    });

    describe('When sent a agreement entity in the request', () => {
        var status;

        beforeAll(async () => {
            var response = await request.post('/agreement')
                                        .set('Content-type', 'application/json')
                                        .send({
                                            "organisation": {
                                                "odsCode": TEST_ODS_CODE,
                                                "name": "MyTestName",
                                                "addressLine1": "Test1",
                                                "addressLine2": "Test2",
                                                "town": "TestTown",
                                                "county": "TestCounty",
                                                "country": "TestCountry",
                                                "postCode": "TestBA11DS",
                                            },
                                            "useCase": "Testing my Service",
                                            "signatory": {
                                                "email": "string",
                                                "name": "string",
                                                "position": "string"
                                            },
                                            "softwareSupplierName": "supplier",
                                            "interactions": {
                                                "appointmentManagementEnabled": true,
                                                "accessRecordHTMLEnabled": true,
                                                "sendDocumentEnabled": false,
                                                "structuredRecordEnabled": false,
                                            }
                                        });
          
            status = response.status;
        });

        it('returns 201', () => {
            expect(status).toBe(201);
        });

        describe('persists the entity into the database', () => {
            var client; 
            var site_definition_response;
            
            beforeAll(async () => {
                client = new Client({
                    user: 'postgres',
                    host: 'localhost',
                    database: 'postgres',
                    port: 5432,
                })
                await client.connect();

                site_definition_response = await client.query(`SELECT * FROM application.site_definition WHERE site_ods_code = '${TEST_ODS_CODE}'`);
                
            });

            afterAll(async () => {
                await client.query(`DELETE FROM application.site_definition WHERE site_ods_code = '${TEST_ODS_CODE}'`);
                await client.end();
            });

            it('site-entity is created', async () => {
                expect(site_definition_response.rowCount).toBe(1);
            });

            describe('site-attributes are created', () => {
                var siteAttributes;
                var site_definition_id_for_query;
                var site_definition_ids_for_deletion;
                
                beforeAll(async () => {
                    site_definition_id_for_query = site_definition_response.rows[0].site_definition_id;
                    
                    site_definition_ids_for_deletion = site_definition_response.rows.map(x => x.site_definition_id).join(', ');
                    
                    var response = await client.query(`SELECT * FROM application.site_attribute WHERE site_definition_id = '${site_definition_id_for_query}'`);
                
                    siteAttributes = response.rows.reduce((map, item) => {
                        map[item.site_attribute_name] = item;
                        return map;
                    }, {});
                });
                
                it('creates ods attribute', () => {
                    expect(siteAttributes['OdsCode'].site_attribute_value).toEqual(TEST_ODS_CODE);
                });

                it('creates name attribute', () => {
                    expect(siteAttributes['SiteName'].site_attribute_value).toEqual('MyTestName');
                });

                it('creates address line 1 attribute', () => {
                    expect(siteAttributes['SiteAddressLine1'].site_attribute_value).toEqual('Test1');
                });

                it('creates address line 2 attribute', () => {
                    expect(siteAttributes['SiteAddressLine2'].site_attribute_value).toEqual('Test2');
                });

                it('creates town attribute', () => {
                    expect(siteAttributes['SiteAddressTown'].site_attribute_value).toEqual('TestTown');
                });

                it('creates county attribute', () => {
                    expect(siteAttributes['SiteAddressCounty'].site_attribute_value).toEqual('TestCounty');
                });
                
                it('creates country attribute', () => {
                    expect(siteAttributes['SiteAddressCountry'].site_attribute_value).toEqual('TestCountry');
                });

                it('creates postcode attribute', () => {
                    expect(siteAttributes['SitePostcode'].site_attribute_value).toEqual('TestBA11DS');
                });

                it('creates use case attribute', () => {
                    expect(siteAttributes['UseCaseDescription'].site_attribute_value).toEqual('Testing my Service');
                });

                it('records the access record html use', () => {
                    expect(siteAttributes['IsHtmlEnabled'].site_attribute_value).toEqual('True');
                });

                it('records the appointment management use', () => {
                    expect(siteAttributes['IsAppointmentEnabled'].site_attribute_value).toEqual('True');
                });

                it('records the appointment management use', () => {
                    expect(siteAttributes['IsStructuredEnabled'].site_attribute_value).toEqual('False');
                });

                it('records the appointment management use', () => {
                    expect(siteAttributes['IsSendDocumentEnabled'].site_attribute_value).toEqual('False');
                });

                afterAll(async () => {
                    await client.query(`DELETE FROM application.site_attribute WHERE site_definition_id IN (${site_definition_ids_for_deletion})`);
                });
            });
            
        })
    })
});