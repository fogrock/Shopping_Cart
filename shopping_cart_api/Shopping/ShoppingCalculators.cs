namespace ShoppingCart
{
    public static class ShoppingCalculators
    {
        public static MoneyAmount ConvertMoneyAmount(MoneyAmount price, string desiredCcyCode)
        {
            if (price.CurrencyCode == desiredCcyCode) return price;
            var rateCurrentCcy = StaticData.FxRates.FirstOrDefault(x => x.CurrencyCode == price.CurrencyCode);
            if (rateCurrentCcy == null) throw new ShoppingException($"Unable to retrieve fx rate for currency '{price.CurrencyCode}'");

            var rateDesiredCcy = StaticData.FxRates.FirstOrDefault(x => x.CurrencyCode == desiredCcyCode);
            if (rateDesiredCcy == null) throw new ShoppingException($"Unable to retrieve fx rate for currency '{desiredCcyCode}'");

            var desiredAmount = price.Amount * rateCurrentCcy.Rate / rateDesiredCcy.Rate;

            return new MoneyAmount(desiredCcyCode, desiredAmount.Round(2));
        }
    }
}
