var supertest = require('supertest');

var ENDPOINT = process.env.ENDPOINT;

const BAD_REQUEST_STATUS_CODE = 400;
const NOT_FOUND_STATUS_CODE = 404;
const UNSUPPORTED_STATUS_CODE = 405;

describe('Transparency Site', () => {
    
    var request = supertest(ENDPOINT);

    describe('When user attempts to delete', () => {
        it('returns 405', (done, err) => {
            request.delete('/transparency-site')
                .expect(UNSUPPORTED_STATUS_CODE, done);
        })
    })

    describe('When user attempts to retrieve an item', () => {
        describe('with invalid id', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site/id')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });

        describe('with a valid id', () => {
            var status, result;

            beforeAll(async () => {
                var response = await request.get('/transparency-site/011c9fb1-827b-4f0c-8fb3-72575a0108d7');
                result = response.body;
                status = response.status;
            });

            it('status code is 200', () => {
                expect(status).toBe(200);
            })
            
            it('contains expected id', () => {
                expect(result.id).toBe('011c9fb1-827b-4f0c-8fb3-72575a0108d7');
            })

            it('contains expected name', () => {
                expect(result.name).toBe('NHS DIGITAL');
            })

            it('contains expected ODS code', () => {
                expect(result.odsCode).toBe('X26');
            })

            it('contains expected access record enabled', () => {
                expect(result.accessRecordHTMLEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.structuredRecordEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.sendDocumentEnabled).toBe(false);
            })

            it('contains expected access record enabled', () => {
                expect(result.appointmentManagementEnabled).toBe(true);
            })

            it('contains expected postcode', () => {
                expect(result.postcode).toBe('LS1 4AP');
            })

            it('contains expected addressLine1', () => {
                expect(result.addressLine1).toBe('THE LEEDS GOVERNMENT HUB');
            })

            it('contains expected addressLine2', () => {
                expect(result.addressLine2).toBe('7-8 WELLINGTON PLACE');
            })

            it('contains expected town', () => {
                expect(result.town).toBe('LEEDS');
            })

            it('contains expected county', () => {
                expect(result.county).toBe('TestCounty');
            })

            it('contains expected country', () => {
                expect(result.country).toBe('ENGLAND');
            })


            it('contains expected useCase', () => {
                expect(result.useCase).toBe('My Use Case');
            })

            it('contains expected CCG name', () => {
                expect(result.ccgIcbName).toBe('NHS SOLIHULL CCG');
            })

            it('contains expected CCG ODS Code', () => {
                expect(result.ccgIcbOdsCode).toBe('05P');
            })
        });

        
    })

    describe('When user searches', () => {
        describe('with no search fields', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });

        describe('with an invalid start parameter', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site?provider_name=b&start=0')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });

        describe('with an invalid count parameter', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site?provider_name=b&count=0')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });

        describe('with too many search fields', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site?provider_code=a&provider_name=b')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });
    });

    describe('Page is higher than totalResults', () => {
        it ('returns no results and the total count', async () => {
            var { body : { totalResults, results } } = 
                await request.get('/transparency-site?provider_name=NHS DIGITAL&start=2');

            expect(totalResults).toBe(1);
            expect(results).toEqual([]);
        })
    })

    describe('When results are returned to the user', () => {
        describe('each record matches the API schema', () => {
            var status, result, count; 

            beforeAll(async () => {
                var response = await request.get('/transparency-site?provider_name=NHS DIGITAL');
                result = response.body.results[0];
                count = response.body.totalResults;
                status = response.status;
            });

            it('totalResults is one', () => {
                expect(count).toBe(1);
            })

            it('contains expected id', () => {
                expect(result.id).toBe('011c9fb1-827b-4f0c-8fb3-72575a0108d7');
            })
            
            it('status code is 200', () => {
                expect(status).toBe(200);
            })
            
            it('contains expected name', () => {
                expect(result.name).toBe('NHS DIGITAL');
            })

            it('contains expected ODS code', () => {
                expect(result.odsCode).toBe('X26');
            })

            it('contains expected access record enabled', () => {
                expect(result.accessRecordHTMLEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.structuredRecordEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.sendDocumentEnabled).toBe(false);
            })

            it('contains expected access record enabled', () => {
                expect(result.appointmentManagementEnabled).toBe(true);
            })

            it('contains expected postcode', () => {
                expect(result.postcode).toBe('LS1 4AP');
            })
            
            it('contains expected addressLine1', () => {
                expect(result.addressLine1).toBe('THE LEEDS GOVERNMENT HUB');
            })

            it('contains expected addressLine2', () => {
                expect(result.addressLine2).toBe('7-8 WELLINGTON PLACE');
            })

            it('contains expected town', () => {
                expect(result.town).toBe('LEEDS');
            })

            it('contains expected county', () => {
                expect(result.county).toBe('TestCounty');
            })

            it('contains expected country', () => {
                expect(result.country).toBe('ENGLAND');
            })

            it('contains expected useCase', () => {
                expect(result.useCase).toBe('My Use Case');
            })

            it('contains expected CCG name', () => {
                expect(result.ccgIcbName).toBe('NHS SOLIHULL CCG');
            })

            it('contains expected CCG ODS Code', () => {
                expect(result.ccgIcbOdsCode).toBe('05P');
            })
        });
    });
});