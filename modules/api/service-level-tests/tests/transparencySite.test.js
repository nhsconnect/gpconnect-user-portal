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
        it('returns 404', (done, err) => {
            request.get('/transparency-site/id')
                .expect(NOT_FOUND_STATUS_CODE, done);
        })
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
                var response = await request.get('/transparency-site?provider_name=name');
                result = response.body[0];
                status = response.status;
            });

            it('status code is 200', () => {
                expect(status).toBe(200);
            })
            
            it('contains expected name', () => {
                expect(result.name).toBe('Name');
            })

            it('contains expected ODS code', () => {
                expect(result.odsCode).toBe('ODS Code');
            })

            it('contains expected access record enabled', () => {
                expect(result.accessRecordHTMLEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.structuredRecordEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.sendDocumentEnabled).toBe(true);
            })

            it('contains expected access record enabled', () => {
                expect(result.appointmentManagementEnabled).toBe(true);
            })

            it('contains expected postcode', () => {
                expect(result.postcode).toBe('BA1 1DS');
            })

            it('contains expected useCase', () => {
                expect(result.useCase).toBe('Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin varius, dui vitae vehicula cursus, turpis nunc pellentesque magna, nec faucibus urna ante eu velit. Suspendisse varius tempus neque mattis imperdiet.');
            })

            it('contains expected CCG name', () => {
                expect(result.ccgIcbName).toBe('CCG');
            })

            it('contains expected CCG ODS Code', () => {
                expect(result.ccgIcbOdsCode).toBe('CCG Code');
            })
        });
    });
});