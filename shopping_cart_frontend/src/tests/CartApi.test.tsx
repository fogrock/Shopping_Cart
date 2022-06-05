import { CartApi } from '../shopping/CartApi';
import { Product, MoneyAmount } from '../shopping/ApiProxy';
const cartApi = new CartApi();
const cart = cartApi.loadCart();
const product = {
    price: { amount: 10, currencyCode: "AUD" },
    productDescription: "sss",
    productId: 23,
    productName: "sdsd"
};


describe('testing retrieving list of countries', () => {
    test('expect to receive a defined object', () => {
        expect(cart).toBeDefined();
        expect(cart).toBeInstanceOf(Object);
    });
});

describe('testing retrieving list of countries', () => {
    test('expect to receive a defined object', () => {
        expect(cart.cartItems).toBeDefined();
        expect(cart.cartItems).toBeInstanceOf(Object);
    });
});