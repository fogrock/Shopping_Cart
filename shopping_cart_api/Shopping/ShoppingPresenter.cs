namespace ShoppingCart
{
    public static class ShoppingPresenter
    {
        public static IEnumerable<Product> GetProductsList(string countryName)
        {            
            var ccyCode = GetCountryCcyCode(countryName);
            return StaticData.Products.Select(p =>
            {
                p.Price = ShoppingCalculators.ConvertMoneyAmount(p.Price, ccyCode);
                return p;
            });
        }

        public static string GetCountryCcyCode(string countryName)
        {
            Country? country = StaticData.Countries.FirstOrDefault(x => x.CountryName == countryName);
            if (country == null) throw new ShoppingException($"Unable to retrieve currency code for country '{countryName}'");
            return country.CurrencyCode;
        }

        public static Cart GetUpdatedCart(CartUpdateRequest request)
        {
            var ccyCode = GetCountryCcyCode(request.CountryName);
            var cartItems = request.Cart.CartItems.Select(x =>
            {
                x.Product.Price = ShoppingCalculators.ConvertMoneyAmount(x.Product.Price, ccyCode);
                return x;
            });
            var cart = new Cart(cartItems);
            return cart;
        }

        public static MoneyAmount GetShippingCost(decimal subTotal, string countryName)
        {
            var ccyCode = GetCountryCcyCode(countryName);
            var audAmount = ShoppingCalculators.ConvertMoneyAmount(new MoneyAmount(ccyCode, subTotal), "AUD");
            var audShippingCost = audAmount.Amount == 0m ? 0m : (audAmount.Amount > 50m) ? 20m : 10m;
            return ShoppingCalculators.ConvertMoneyAmount(new MoneyAmount("AUD", audShippingCost), ccyCode);
        }

        public static bool ProcessOrder(Order order)
        {
            if (order.Cart != null && !string.IsNullOrEmpty(order.OrderId) && !string.IsNullOrEmpty(order.OrderIsoDate) && !string.IsNullOrEmpty(order.CountryName)) return true;
            else return false;
        }
    }
}
