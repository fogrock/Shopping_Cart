import './App.css';
import React, { useEffect } from 'react';
import Alert from '@mui/material/Alert';
import { ApiProxy } from './shopping/ApiProxy';
import InputLabel from '@mui/material/InputLabel';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import { Products } from './shopping/Products';
import { CartView } from './shopping/CartView';
import Button from '@mui/material/Button';
import { ThankYou } from './shopping/ThankYou';

function App() {
    const [error, setError] = React.useState<string | undefined>(undefined);
    const [countries, setCountries] = React.useState<string[]>(['Australia']);
    const [countryName, setCountryName] = React.useState<string>('Australia');
    const [showProducts, setShowProducts] = React.useState<boolean>(true);
    const [showCart, setShowCart] = React.useState<boolean>(false);
    const [showThankYou, setShowThankYou] = React.useState<boolean>(false);

    useEffect(() => {
        getCountryList();
    }, []);

    const getCountryList = () => {
        const apiProxy = new ApiProxy();
        apiProxy.getCountryList()
            .then(result => setCountries(result))
            .catch(error => setError(error.response.data))
    }

    const onCountryChange = (event: SelectChangeEvent<typeof countryName>) => {
        const { target: { value } } = event;
        setCountryName(value);
    };

    const onGoToCart = () => {
        setShowProducts(false);
        setShowCart(true);
        setShowThankYou(false)
    }

    const onGoToProduct = () => {
        setShowProducts(true);
        setShowCart(false);
        setShowThankYou(false)
    }

    const onOrderProcessed = () => {
        setShowProducts(false);
        setShowCart(false);
        setShowThankYou(true)
    }

    return (
        <div>
            {error ? <div className="paddedUpDown"><Alert variant='outlined' severity='error'>Error: {error}</Alert></div> : null}
            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                <InputLabel>Country</InputLabel>
                <Select
                    style={{ width: '200px', marginBottom: '10px' }}
                    labelId="country-label"
                    id="country-select"
                    value={countryName}
                    label="Country"
                    onChange={onCountryChange}
                >
                    {countries.map((country) => (
                        <MenuItem key={country} value={country}>{country}</MenuItem>
                    ))}
                </Select>
            </FormControl>
            {showProducts ? <Button variant="contained" onClick={onGoToCart} className='mainButton'>Go to shopping cart</Button> : null}
            {showCart || showThankYou ? <Button variant="contained" onClick={onGoToProduct} className='mainButton'>Go to product list</Button> : null}
            {showProducts ? <Products countryName={countryName} /> : null}
            {showCart ? <CartView countryName={countryName} onOrderProcessed={onOrderProcessed} /> : null}
            {showThankYou ? <ThankYou /> : null}
        </div>
    );
}

export default App;
