namespace ShoppingCart
{
    public class Country
    {
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }

        public Country(string countryName, string currencyCode)
        {
            CountryName = countryName;
            CurrencyCode = currencyCode;
        }
    }

    public class FxRate
    {
        public string CurrencyCode { get; set; }
        public decimal Rate { get; set; }

        public FxRate(string currencyCode, decimal rate)
        {
            CurrencyCode = currencyCode;
            Rate = rate;
        }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public MoneyAmount Price { get; set; }

        public Product(int productId, string productName, string productDescription, MoneyAmount price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }
    }

    public class MoneyAmount
    {
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }

        public MoneyAmount(string currencyCode, decimal amount)
        {
            CurrencyCode = currencyCode;
            Amount = amount;
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Count { get; set; }

        public CartItem(Product product, int count)
        {
            Product = product;
            Count = count;
        }
    }

    public class Cart
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        public Cart(IEnumerable<CartItem> cartItems)
        {
            CartItems = cartItems;
        }
    }

    public class CartUpdateRequest
    {
        public Cart Cart { get; set; }
        public string CountryName { get; set; }
        public CartUpdateRequest(Cart cart, string countryName)
        {
            Cart = cart;
            CountryName = countryName;
        }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string OrderIsoDate { get; set; }
        public Cart Cart { get; set; }
        public string CountryName { get; set; }

        public Order(string orderId, string orderIsoDate, Cart cart, string countryName)
        {
            OrderId = orderId;
            OrderIsoDate = orderIsoDate;
            Cart = cart;
            CountryName = countryName;
        }
    }

}
