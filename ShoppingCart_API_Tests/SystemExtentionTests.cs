namespace ShoppingCart_API_Tests
{
    [TestClass]
    public class SystemExtentionTests
    {
        [TestMethod]
        [DataRow(10.554, 2, 10.55)]
        [DataRow(10.555, 2, 10.56)]
        [DataRow(10.556, 2, 10.56)]
        [DataRow(10.5554, 3, 10.555)]
        [DataRow(10.5555, 3, 10.556)]
        [DataRow(10.5556, 3, 10.556)]
        public void DecimalRound_GeneralTest(double givenAmount, int precision, double expectedRoundedAmount)
        {
            var calculatedAmount = (decimal)givenAmount;
            var roundedAmount = calculatedAmount.Round(precision);
            Assert.AreEqual((decimal)expectedRoundedAmount, roundedAmount);
        }
    }
}