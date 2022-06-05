namespace ShoppingCart_API_Tests
{
    [TestClass]
    public class ShoppingCalculatorTests
    {
        [TestMethod]
        [DataRow("AUD", 100.45, "AUD", 100.45)]
        [DataRow("AUD", 130.65, "USD", 94.07)]
        [DataRow("AUD", 130.65, "HKD", 723.60)]
        [DataRow("JPY", 457897.78, "GBP", 2784.02)]
        [DataRow("BRL", 9875.65, "USD", 2073.89)]
        [DataRow("EUR", 547.58, "JPY", 77093.50)]
        [DataRow("USD", 110.23, "EUR", 103.02)]
        [DataRow("USD", 268.14, "USD", 268.14)]
        public void ConvertMoneyAmount_GeneralTest(string currentCcy, double currentAmount, string desiredCcy, double expectedAmount)
        {
            MoneyAmount currentMoneyAmount = new MoneyAmount(currentCcy, (decimal)currentAmount);
            MoneyAmount newMoneyAmount = ShoppingCalculators.ConvertMoneyAmount(currentMoneyAmount, desiredCcy);
            Assert.AreEqual(desiredCcy, newMoneyAmount.CurrencyCode);
            Assert.AreEqual((decimal)expectedAmount, newMoneyAmount.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        public void ConvertMoneyAmount_MissingCcyTest1()
        {
            ShoppingCalculators.ConvertMoneyAmount(new MoneyAmount ("AUD", 100m), "QAR");
        }

        [TestMethod]
        [ExpectedException(typeof(ShoppingException))]
        public void ConvertMoneyAmount_MissingCcyTest2()
        {
            ShoppingCalculators.ConvertMoneyAmount(new MoneyAmount("SAR", 100m), "AUD");
        }
    }
}