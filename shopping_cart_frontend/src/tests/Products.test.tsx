import React from 'react';
import { render, screen } from '@testing-library/react';
import { Products } from '../shopping/Products';

test('renders buttons and selector', () => {
    render(<Products countryName="Australia" />);
    const linkElement1 = screen.getByText(/Add to cart/i);
    expect(linkElement1).toBeInTheDocument();
});

it("matches snapshot", () => {
    const { asFragment } = render(<Products countryName="Australia" />);
    expect(asFragment()).toMatchSnapshot();
});