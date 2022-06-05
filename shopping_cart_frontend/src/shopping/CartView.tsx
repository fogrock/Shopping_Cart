import { useEffect } from "react";
import { ApiProxy, CartUpdateRequest, MoneyAmount, Order } from "./ApiProxy";
import { Cart, CartApi, CartItem } from "./CartApi";
import { AgGridReact } from 'ag-grid-react';
import { AgGridEvent, GridApi } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import { createGuid, formatNumber } from '../shared/Utils';
import Alert from '@mui/material/Alert';
import React from "react";
import Button from '@mui/material/Button';
import '../App.css';

export interface CartProps {
    countryName: string;
    onOrderProcessed: () => void;
}

export function CartView(props: CartProps) {
    const apiProxy = new ApiProxy();
    const cartApi = new CartApi();
    const loadedCart = cartApi.loadCart();
    const { countryName, onOrderProcessed } = props;
    const [error, setError] = React.useState<string | undefined>(undefined);
    const [postage, setPostage] = React.useState<MoneyAmount | undefined>(undefined);
    const [selectedProducts, setSelectedProducts] = React.useState<number[]>([]);
    const [gridApi, setGridApi] = React.useState<GridApi | undefined>(undefined);
    const cartRef = React.useRef<Cart>(loadedCart);

    const updateCartModel = (): void => {
        apiProxy.updateCartModel(new CartUpdateRequest(cartRef.current, countryName))
            .then((result) => {
                cartApi.saveNewCart(result);
                const loadedCart = cartApi.loadCart();
                cartRef.current = loadedCart;
                gridApi?.setRowData(cartRef.current.cartItems);
            })
            .catch(error => setError(error.response.data))
    }

    const getPostage = (): void => {
        apiProxy.getPostage(getSubTotal(), countryName)
            .then(result => setPostage(result))
            .catch(error => setError(error.response.data))
    }

    useEffect(() => {
        updateCartModel();
    }, [countryName]);

    useEffect(() => {
        getPostage();
    }, [cartRef.current, countryName]);

    const currencyFormatter = (params: any) => {
        if (params && params.data && params.data.product && params.data.product.price) return `${params.data.product.price.currencyCode} ${formatNumber(params.value)}`;
        return params.value;
    };

    const getAmount = (params: any): number => {
        if (params && params.data && params.data.product && params.data.product.price) return params.data.product.price.amount * params.data.count;
        return 0;
    }

    const onRemove = (): void => {
        setError(undefined);
        if (selectedProducts && selectedProducts.length > 0) {
            selectedProducts.map(x => {
                const removedCartItem: CartItem | undefined = cartRef.current.cartItems.find(z => z.product.productId == x);
                if (removedCartItem != undefined) cartRef.current.removeItemFromCartAll(removedCartItem.product);
            })
        }
        setSelectedProducts([]);
        const loadedCart = cartApi.loadCart();
        cartRef.current = loadedCart
        gridApi?.setRowData(cartRef.current.cartItems);
    }

    const onOrder = (): void => {
        setError(undefined);
        const date = new Date();
        const orderId = createGuid();
        apiProxy.placeOrder(new Order(orderId, date.toISOString(), cartRef.current, countryName))
            .then((result) => {
                if (result == true) {
                    sessionStorage.removeItem('shoppingCart');
                    sessionStorage.setItem('lastOrderId', orderId);
                    onOrderProcessed();
                }
                else {
                    setError("Failed to process the order, please try again");
                }
            })
            .catch(error => setError(error.response.data))
    }

    const gridOptions = {
        columnDefs: [
            { checkboxSelection: true, width: 50 },
            { field: 'product.productName', headerName: "Name", width: 400 },
            { field: 'product.price.amount', headerName: "Price", width: 200, cellStyle: { textAlign: 'right' }, valueFormatter: currencyFormatter },
            { field: 'count', headerName: "Count", width: 200, cellStyle: { textAlign: 'right' } },
            { headerName: "Amount", width: 200, cellStyle: { textAlign: 'right' }, valueGetter: getAmount, valueFormatter: currencyFormatter },
        ],
        defaultColDef: {
            sortable: true,
            wrapText: true,
            autoHeight: true,
        },
        rowSelection: 'multiple',
        onSelectionChanged: onSelectionChanged,
        onGridReady: onCartGridReady
    };

    function onCartGridReady(params: any): void {
        setGridApi(params.api);
    }

    function onSelectionChanged(evt: AgGridEvent) {
        setError(undefined);
        const selectedRows: CartItem[] = evt.api.getSelectedRows();
        const selectedProductIds: number[] = selectedRows.map(x => x.product.productId)
        setSelectedProducts(selectedProductIds);
    }

    const getSubTotal = (): number => {
        const subTotal = cartRef.current.cartItems.map(x => x.count * x.product.price.amount).reduce((a, b) => a + b, 0);
        return subTotal;
    }

    const getSubTotalStr = (): string => {
        const ccyCode = cartRef.current.cartItems.length > 0 ? cartRef.current.cartItems[0].product.price.currencyCode : "";
        return `${ccyCode} ${formatNumber(getSubTotal())}`;
    }

    const getPostageStr = (): string => {
        return postage == undefined ? "" : `${postage.currencyCode} ${formatNumber(postage.amount)}`;
    }

    const getTotalStr = (): string => {
        const ccyCode = cartRef.current.cartItems.length > 0 ? cartRef.current.cartItems[0].product.price.currencyCode : "";
        const shippingCost = postage == undefined ? 0 : postage.amount;
        const total = getSubTotal() + shippingCost;
        return `${ccyCode} ${formatNumber(total)}`;
    }

    return (
        <div>
            {error ? <div className="paddedUpDown"><Alert variant='outlined' severity='error'>Error: {error}</Alert></div> : null}
            <Button variant="contained" onClick={onRemove} disabled={!(selectedProducts && selectedProducts.length > 0)} >Remove from cart</Button>
            <Button variant="contained" onClick={onOrder} disabled={!(cartRef.current.cartItems.length > 0) || error != undefined} color="success" style={{ float: 'right' }}>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Place Order&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</Button>
            <div className="ag-theme-alpine paddedUpDown">
                <AgGridReact gridOptions={gridOptions} rowData={cartRef.current.cartItems} domLayout={'autoHeight'}></AgGridReact>
            </div>
            <div className='cartTotals' >Subtotal:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{getSubTotalStr()}</div>
            <div className='cartPostage' >Postage:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{getPostageStr()}</div>
            <div className='cartTotals' >Total:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{getTotalStr()}</div>
        </div>
    );
}