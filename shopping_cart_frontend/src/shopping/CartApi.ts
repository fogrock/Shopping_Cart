import { Product } from "./ApiProxy";

export class CartItem {
    product: Product;
    count: number;

    constructor(product: Product, count: number) {
        this.product = product;
        this.count = count;
    }
}

export class Cart {
    cartItems: CartItem[];
    constructor(cartItems: CartItem[]) {
        this.cartItems = cartItems;
    }

    saveCart(): void {
        sessionStorage.setItem('shoppingCart', JSON.stringify(this.cartItems));
    }

    addItemToCart(product: Product, count: number): void {
        const item: CartItem | undefined = this.cartItems.find(item => item.product.productId === product.productId && item.product.productName === product.productName);
        if (item != undefined) {
            item.count++;
        }
        else {
            const newItem: CartItem = new CartItem(product, count);
            this.cartItems.push(newItem);
        }
        this.saveCart();
    }

    removeItemFromCartAll(product: Product) {
        this.cartItems.map
            (item => {
                if (item.product.productId === product.productId && item.product.productName === product.productName) {
                    this.cartItems.splice(this.cartItems.indexOf(item), 1);
                    this.saveCart();
                    return;
                };
            });
    }
}

export class CartApi {
    loadCart(): Cart {
        const cart: Cart = new Cart([]);
        const cartString: string | null = sessionStorage.getItem('shoppingCart')
        if (typeof cartString === "string") cart.cartItems = JSON.parse(cartString);
        cart.cartItems.sort(function (a, b) {
            const productName1: string = a.product.productName.toUpperCase();
            const productName2: string = b.product.productName.toUpperCase();
            return (productName1 < productName2) ? -1 : (productName1 > productName2) ? 1 : 0;
        });
        return cart;
    }

    saveNewCart(cart: Cart): void {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart.cartItems));
    }
}