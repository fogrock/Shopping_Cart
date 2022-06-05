import Typography from '@mui/material/Typography';

export function ThankYou() {
    return (
        <div>
            <Typography variant="h3" gutterBottom component="div">
                Thank you.
            </Typography>
            <Typography variant="h5" gutterBottom component="div">
                Your order was completed successfully
            </Typography>
            <Typography variant="button" display="block" gutterBottom>
                order id: {sessionStorage.getItem('lastOrderId')}
            </Typography>
        </div>
    );
}