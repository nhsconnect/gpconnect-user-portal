var supertest = require('supertest');

var ENDPOINT = process.env.ENDPOINT;

describe('HealthCheck', () => {
    describe('When Service is healthy', () => {
        it('returns 200', (done, err) => {
            supertest(ENDPOINT)
                .get('/health')
                .expect(200, done);
        })
    })
});