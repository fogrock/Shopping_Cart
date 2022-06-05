# Shopping_Cart
Test React "Shopping Cart" application with a C# backend.
 
The application displays a list of products to the user. The user has 
the ability to add these items to a virtual basket and have the front end 
update.
 
The products are retrieved from a web API call to a back end. A 
static list of products is used. Product prices are demoninated in AUD currency.
 
Once the products are added to the basket the user can click 'go to 
basket' button to perform a check out of the products.
 
The checkout page presents the basket items to the user. The user can remove
items from the basket there.
 
The checkout page will calls a backend to calculate the total shipping cost. $10 
shipping cost for orders less of $50 dollars and less. $20 for orders more than 
$50.

Website also contains a country selector. Based on the country selected all
product prices as well as basket amounts and postage are recalculated and displayed
in the correct currency.
 
The checkout page has a 'place order' button which post all the products to a method
and will returns 'success' to the page. Once that is complete the page navigates to a 'thank you' page.
