import axios from "axios";
import { Cart } from "./CartApi";

const baseUrl: string = "https://localhost:7267";

export interface MoneyAmount {
    amount: number;
    currencyCode: string;
}

export interface Product {
    productId: number,
    productName: string,
    productDescription: string,
    price: MoneyAmount
}

export class CartUpdateRequest {
    cart: Cart;
    countryName: string;
    constructor(cart: Cart, countryName: string) {
        this.cart = cart;
        this.countryName = countryName;
    }
}

export class Order {
    orderId: string;
    orderIsoDate: string;
    cart: Cart;
    countryName: string;
    constructor(orderId: string, orderIsoDate: string, cart: Cart, countryName: string) {
        this.orderId = orderId;
        this.orderIsoDate = orderIsoDate;
        this.cart = cart;
        this.countryName = countryName;
    }
}

export class ApiProxy {

    getProductList(countryName: string): Promise<Product[]> {
        return axios.get(`${baseUrl}/Product`, {
            params: {
                countryName: countryName
            }
        })
            .then(result => result.data);
    }

    getCountryList(): Promise<string[]> {
        return axios.get(`${baseUrl}/Country`)
            .then(result => result.data);
    }

    updateCartModel(request: CartUpdateRequest): Promise<Cart> {
        return axios.post(`${baseUrl}/CartUpdate`, request)
            .then(result => { return result.data; });
    }

    getPostage(subTotal: number, countryName: string): Promise<MoneyAmount> {
        return axios.get(`${baseUrl}/Postage`, {
            params: {
                subTotal: subTotal,
                countryName: countryName
            }
        })
            .then(result => result.data);
    }

    placeOrder(order: Order): Promise<boolean> {
        return axios.post(`${baseUrl}/PlaceOrder`, order)
            .then(result => { return result.data; });
    }
}