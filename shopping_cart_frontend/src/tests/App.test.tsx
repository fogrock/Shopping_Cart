import React from 'react';
import { render, screen } from '@testing-library/react';
import App from '../App';

test('renders buttons and selector', () => {
    render(<App />);
    const linkElement1 = screen.getByText(/Go to shopping cart/i);
    expect(linkElement1).toBeInTheDocument();
    const linkElement2 = screen.getByText(/Country/i);
    expect(linkElement2).toBeInTheDocument();
});

it("matches snapshot", () => {
    const { asFragment } = render(<App />);
    expect(asFragment()).toMatchSnapshot();
});