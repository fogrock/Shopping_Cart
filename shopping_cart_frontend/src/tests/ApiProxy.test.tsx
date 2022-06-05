import { ApiProxy } from '../shopping/ApiProxy';
const apiProxy = new ApiProxy();

describe('test retrieving list of countries', () => {
    test('expect to receive a defined object', () => {
        expect(apiProxy.getCountryList()).toBeDefined();
        expect(apiProxy.getCountryList()).toBeInstanceOf(Object);
    });
});

describe('testing retrieving card shipping costs', () => {
    test('expect to receive a defined object', () => {
        expect(apiProxy.getPostage(450.45, "France")).toBeDefined();
        expect(apiProxy.getPostage(450.45, "France")).toBeInstanceOf(Object);
    });
});

describe('testing retrieving list of products', () => {
    test('expect to receive a defined object', () => {
        expect(apiProxy.getProductList("France")).toBeDefined();
        expect(apiProxy.getProductList("France")).toBeInstanceOf(Object);
    });
});