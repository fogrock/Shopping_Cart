namespace ShoppingCart_API_Tests
{
    [TestClass]
    public class ShoppingPresenterTests
    {
        [TestMethod]
        public void GetProductsList_GeneralTest()
        {
            var products = ShoppingPresenter.GetProductsList("France");
            Assert.IsNotNull(products);
            Assert.AreEqual(10, products.Count());
            Assert.AreEqual("EUR", products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Price.CurrencyCode);
            Assert.AreEqual(10.36m, products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Price.Amount);
            Assert.AreEqual("EUR", products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips").Price.CurrencyCode);
            Assert.AreEqual(28.67m, products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips").Price.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        public void GetProductsList_UnknownCountry()
        {
            var products = ShoppingPresenter.GetProductsList("Canada");
        }

        [TestMethod]
        [DataRow("Australia", "AUD")]
        [DataRow("Germany", "EUR")]
        [DataRow("France", "EUR")]
        [DataRow("Hong Kong", "HKD")]
        [DataRow("Japan", "JPY")]
        [DataRow("Brazil", "BRL")]
        [DataRow("Great Britain", "GBP")]
        [DataRow("United States", "USD")]
        public void GetCountryCcyCodet_GeneralTest(string country, string expectedCcyCode)
        {
            var ccyCode = ShoppingPresenter.GetCountryCcyCode(country);
            Assert.AreEqual(expectedCcyCode, ccyCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        [DataRow("Canada", "CAD")]
        [DataRow("Italy", "EUR")]
        public void GetCountryCcyCodet_MissingCountries(string country, string expectedCcyCode)
        {
            var ccyCode = ShoppingPresenter.GetCountryCcyCode(country);
            Assert.AreEqual(expectedCcyCode, ccyCode);
        }

        [TestMethod]
        public void GetUpdatedCart_GeneralTest1()
        {
            var products = ShoppingPresenter.GetProductsList("Brazil");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            CartUpdateRequest request = new CartUpdateRequest(cart, "Great Britain");
            var newCart = ShoppingPresenter.GetUpdatedCart(request);
            Assert.IsNotNull(newCart);
            Assert.AreEqual(2, newCart.CartItems.Count());
            Assert.AreEqual(3, newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Count);
            Assert.AreEqual(5, newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Count);
            Assert.AreEqual("GBP", newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Product.Price.CurrencyCode);
            Assert.AreEqual("GBP", newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Product.Price.CurrencyCode);
            Assert.AreEqual(8.87m, newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Product.Price.Amount);
            Assert.AreEqual(24.54m, newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Product.Price.Amount);
        }

        [TestMethod]
        public void GetUpdatedCart_GeneralTest2()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            CartUpdateRequest request = new CartUpdateRequest(cart, "Australia");
            var newCart = ShoppingPresenter.GetUpdatedCart(request);
            Assert.IsNotNull(newCart);
            Assert.AreEqual(2, newCart.CartItems.Count());
            Assert.AreEqual(3, newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Count);
            Assert.AreEqual(5, newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Count);
            Assert.AreEqual("AUD", newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Product.Price.CurrencyCode);
            Assert.AreEqual("AUD", newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Product.Price.CurrencyCode);
            Assert.AreEqual(15.40m, newCart.CartItems.First(x => x.Product.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp").Product.Price.Amount);
            Assert.AreEqual(42.60m, newCart.CartItems.First(x => x.Product.ProductName == "NWS 250mm Left Aviation Snips").Product.Price.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        public void GetUpdatedCart_MissingCountry()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            CartUpdateRequest request = new CartUpdateRequest(cart, "Canada");
            var newCart = ShoppingPresenter.GetUpdatedCart(request);
        }

        [TestMethod]
        [DataRow(0.00, "Australia", "AUD", 0.00)]
        [DataRow(12.45, "Australia", "AUD", 10.00)]
        [DataRow(49.99, "Australia", "AUD", 10.00)]
        [DataRow(50.00, "Australia", "AUD", 10.00)]
        [DataRow(50.01, "Australia", "AUD", 20.00)]
        [DataRow(250.01, "Australia", "AUD", 20.00)]
        [DataRow(0.00, "United States", "USD", 0.00)]
        [DataRow(15.50, "United States", "USD", 7.20)]
        [DataRow(35.25, "United States", "USD", 7.20)]
        [DataRow(36.00, "United States", "USD", 7.20)]
        [DataRow(36.01, "United States", "USD", 14.40)]
        [DataRow(176.01, "United States", "USD", 14.40)]
        [DataRow(0.00, "Japan", "JPY", 0.00)]
        [DataRow(395.50, "Japan", "JPY", 947.37)]
        [DataRow(4452.95, "Japan", "JPY", 947.37)]
        [DataRow(4736.84, "Japan", "JPY", 947.37)]
        [DataRow(4737.79, "Japan", "JPY", 1894.74)]
        [DataRow(48752.95, "Japan", "JPY", 1894.74)]
        public void GetShippingCost_GeneralTest(double orderSubTotal, string country, string expectedCcy, double expectedShippingAmount)
        {
            var calculatedShipping = ShoppingPresenter.GetShippingCost((decimal)orderSubTotal, country);
            Assert.IsNotNull(calculatedShipping);
            Assert.AreEqual(expectedCcy, calculatedShipping.CurrencyCode);
            Assert.AreEqual((decimal)expectedShippingAmount, calculatedShipping.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        [DataRow(250.01, "Canada", "CAD", 20.00)]
        [DataRow(250.01, "Italy", "EUR", 20.00)]
        public void GetShippingCost_MissingCountry(double orderSubTotal, string country, string expectedCcy, double expectedShippingAmount)
        {
            var calculatedShipping = ShoppingPresenter.GetShippingCost((decimal)orderSubTotal, country);
        }

        [TestMethod]
        public void ProcessOrder_GeneralTest1()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            Order order = new Order("mockOrderId", "mockOrderIsoDate", cart, "Australia");
            var orderStatus = ShoppingPresenter.ProcessOrder(order);
            Assert.IsTrue(orderStatus);
        }

        [TestMethod]
        public void ProcessOrder_GeneralTest2()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            Order order = new Order(String.Empty, "mockOrderIsoDate", cart, "Australia");
            var orderStatus = ShoppingPresenter.ProcessOrder(order);
            Assert.IsFalse(orderStatus);
        }

        [TestMethod]
        public void ProcessOrder_GeneralTest3()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            Order order = new Order("mockOrderId", String.Empty, cart, "Australia");
            var orderStatus = ShoppingPresenter.ProcessOrder(order);
            Assert.IsFalse(orderStatus);
        }

        [TestMethod]
        public void ProcessOrder_GeneralTest4()
        {
            var products = ShoppingPresenter.GetProductsList("Australia");
            CartItem item1 = new CartItem(products.First(x => x.ProductName == "Craftright 300mm 2 Piece Quick Action Clamp"), 3);
            CartItem item2 = new CartItem(products.First(x => x.ProductName == "NWS 250mm Left Aviation Snips"), 5);
            Cart cart = new Cart(new List<CartItem> { item1, item2 });
            Order order = new Order("mockOrderId", "mockOrderIsoDate", cart, String.Empty);
            var orderStatus = ShoppingPresenter.ProcessOrder(order);
            Assert.IsFalse(orderStatus);
        }
    }
}