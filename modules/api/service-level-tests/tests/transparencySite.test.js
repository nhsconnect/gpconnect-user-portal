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
                expect(result.postcode).toBe('BA1 1DS');
            })

            it('contains expected useCase', () => {
                expect(result.useCase).toBe('My Use Case');
            })

            it('contains expected CCG name', () => {
                expect(result.ccgIcbName).toBe('CCG Name');
            })

            it('contains expected CCG ODS Code', () => {
                expect(result.ccgIcbOdsCode).toBe('CCG Code');
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

        describe('with too many search fields', () => {
            it('returns 400', (done, err) => {
                request.get('/transparency-site?provider_code=a&provider_name=b')
                    .expect(BAD_REQUEST_STATUS_CODE, done);
            })
        });
    });

    describe('When results are returned to the user', () => {
        describe('each record matches the API schema', () => {
            var status, result; 

            beforeAll(async () => {
                var response = await request.get('/transparency-site?provider_name=NHS DIGITAL');
                result = response.body[0];
                status = response.status;
            });

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
                expect(result.postcode).toBe('BA1 1DS');
            })

            it('contains expected useCase', () => {
                expect(result.useCase).toBe('My Use Case');
            })

            it('contains expected CCG name', () => {
                expect(result.ccgIcbName).toBe('CCG Name');
            })

            it('contains expected CCG ODS Code', () => {
                expect(result.ccgIcbOdsCode).toBe('CCG Code');
            })
        });
    });
});