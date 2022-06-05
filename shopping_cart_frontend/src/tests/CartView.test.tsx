import React from 'react';
import { render, screen } from '@testing-library/react';
import { CartView } from '../shopping/CartView';

test('renders buttons and selector', () => {
    render(<CartView countryName="Australia" onOrderProcessed={function (): void {
        throw new Error('Function not implemented.');
    }} />);
    const linkElement1 = screen.getByText(/Remove from cart/i);
    expect(linkElement1).toBeInTheDocument();
    const linkElement2 = screen.getByText(/Place Order/i);
    expect(linkElement2).toBeInTheDocument();
    const linkElement3 = screen.getByText(/Subtotal:/i);
    expect(linkElement3).toBeInTheDocument();
    const linkElement4 = screen.getByText(/Postage:/i);
    expect(linkElement4).toBeInTheDocument();
});

it("matches snapshot", () => {
    const { asFragment } = render(<CartView countryName="Australia" onOrderProcessed={function (): void {
        throw new Error('Function not implemented.');
    }} />);
    expect(asFragment()).toMatchSnapshot();
});