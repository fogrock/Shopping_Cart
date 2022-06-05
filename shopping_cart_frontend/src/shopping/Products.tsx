import React from 'react';
import { ApiProxy, Product } from './ApiProxy';
import { useEffect } from 'react';
import Alert from '@mui/material/Alert';
import { AgGridReact } from 'ag-grid-react';
import { AgGridEvent, GridApi } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import Button from '@mui/material/Button';
import { Cart, CartApi } from './CartApi';
import { formatNumber } from '../shared/Utils';
import '../App.css';

export interface ProductsProps {
    countryName: string
}

export function Products(props: ProductsProps) {
    const { countryName } = props;
    const [error, setError] = React.useState<string | undefined>(undefined);
    const [products, setProducts] = React.useState<Product[]>([]);
    const [selectedProducts, setSelectedProducts] = React.useState<number[]>([]);
    const [gridApi, setGridApi] = React.useState<GridApi | undefined>(undefined);
    const currencyFormatter = (params: any) => {
        if (params && params.data) return `${params.data.price.currencyCode} ${formatNumber(params.value)}`;
        return params.value;
    };

    const gridOptions = {
        columnDefs: [
            { checkboxSelection: true, width: 50 },
            { field: 'productName', headerName: "Name", width: 400 },
            { field: 'productDescription', headerName: "Description", width: 600, resizable: true, flex: 1 },
            { field: 'price.amount', headerName: "Price", width: 180, cellStyle: { textAlign: 'right' }, valueFormatter: currencyFormatter }
        ],
        defaultColDef: {
            sortable: true,
            wrapText: true,
            autoHeight: true,
        },
        rowSelection: 'multiple',
        onSelectionChanged: onSelectionChanged,
        onGridReady: onProductsGridReady,
    };

    function onProductsGridReady(params: any): void {
        setGridApi(params.api);
    }

    function onSelectionChanged(evt: AgGridEvent) {
        setError(undefined);
        const selectedRows: Product[] = evt.api.getSelectedRows();
        const selectedProductIds: number[] = selectedRows.map(x => x.productId)
        setSelectedProducts(selectedProductIds);
    }

    const getProductList = () => {
        const apiProxy = new ApiProxy();
        apiProxy.getProductList(countryName)
            .then(result => setProducts(result))
            .catch(error => setError(error.response.data))
    }

    const onAdd = () => {
        setError(undefined);
        const cartApi = new CartApi();
        const cart: Cart = cartApi.loadCart();
        if (selectedProducts && selectedProducts.length > 0) {
            selectedProducts.map(x => {
                const addedProduct: Product | undefined = products.find(z => z.productId == x);
                if (addedProduct != undefined) cart.addItemToCart(addedProduct, 1);
            })
        }
        setSelectedProducts([]);
        gridApi?.deselectAll();
    }

    useEffect(() => {
        getProductList();
    }, [countryName]);

    return (
        <div>
            {error ? <div className="paddedUpDown"><Alert variant='outlined' severity='error'>Error: {error}</Alert></div> : null}
            <div className="ag-theme-alpine paddedUpDown">
                <AgGridReact gridOptions={gridOptions} rowData={products} domLayout={'autoHeight'}></AgGridReact>
            </div>
            <Button variant="contained" onClick={onAdd} disabled={!(selectedProducts && selectedProducts.length > 0)} >Add to cart</Button>
        </div>
    );
}