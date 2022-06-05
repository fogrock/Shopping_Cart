namespace ShoppingCart
{
    public static class SystemExtentions
    {
        public static decimal Round(this decimal val, int num)
        {
            return decimal.Round(val, num, MidpointRounding.AwayFromZero);
        }
    }
}
