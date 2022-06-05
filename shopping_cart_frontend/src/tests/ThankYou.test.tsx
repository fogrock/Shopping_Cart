import React from 'react';
import { render, screen } from '@testing-library/react';
import { ThankYou } from '../shopping/ThankYou';

test('renders buttons and selector', () => {
    render(<ThankYou />);
    const linkElement1 = screen.getByText(/Thank you./i);
    expect(linkElement1).toBeInTheDocument();
    const linkElement2 = screen.getByText(/Your order was completed successfully/i);
    expect(linkElement2).toBeInTheDocument();
    const linkElement3 = screen.getByText(/order id:/i);
    expect(linkElement3).toBeInTheDocument();
});

it("matches snapshot", () => {
    const { asFragment } = render(<ThankYou />);
    expect(asFragment()).toMatchSnapshot();
});