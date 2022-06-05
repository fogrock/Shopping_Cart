using System.Data;

namespace ShoppingCart
{
    internal static class StaticData
    {
        internal static readonly List<Country> Countries = new Dictionary<string, string> {
            { "Australia", "AUD" },
            { "Germany", "EUR" },
            { "France", "EUR" },
            { "Hong Kong", "HKD" },
            { "Japan", "JPY" },
            { "Brazil", "BRL" },
            { "Great Britain", "GBP" },
            { "United States", "USD" },
        }
        .Select(x => new Country(x.Key, x.Value))
        .OrderBy(x => x.CountryName)
        .ToList();

        internal static readonly List<FxRate> FxRates = new Dictionary<string, decimal> {
            { "AUD", 0.72m },
            { "EUR", 1.07m },
            { "HKD", 0.13m },
            { "JPY", 0.0076m },
            { "BRL", 0.21m },
            { "GBP", 1.25m },
            { "USD", 1.00m },
        }
        .Select(x => new FxRate(x.Key, x.Value))
        .ToList();

        private static readonly Dictionary<string, decimal> ProductAUDPrices = new Dictionary<string, decimal>
        {
            { "Irwin 20oz Fibreglass Hammer", 20.45m },
            { "Estwing 4.5kg Soft Face Sledge Hammer", 35.48m },
            { "Kincrome 12' 300mm Heavy Duty Hacksaw", 54.90m },
            { "Spear & Jackson 550mm Hand Saw", 65.12m },
            { "NWS 250mm Left Aviation Snips", 42.60m },
            { "Craftright 300mm 2 Piece Quick Action Clamp", 15.40m },
            { "Kincrome 8 Piece Mini Plier Set", 125.95m },
            { "Will Tie Wire Cutters", 71.85m },
            { "Victa 46' 23HP Zero Turn Petrol Ride On Mower", 6749.00m },
            { "Mitsubishi Bronte 8.0kW Reverse Cycle Split System A/C", 2349.00m }
        };

        private static MoneyAmount GetProductAUDPrice(string productName)
        {
            decimal price;
            if (!ProductAUDPrices.TryGetValue(productName, out price))
            {
                return new MoneyAmount("AUD", 10m);
            }
            return new MoneyAmount("AUD", price);
        }

        internal static readonly List<Product> Products = new Dictionary<string, string> {
            { "Irwin 20oz Fibreglass Hammer", "The IRWIN 20oz Fibreglass Hammer features a ProTouch grip for comfort and control, while its durable lightweight fibreglass handle helps limit vibration." },
            { "Estwing 4.5kg Soft Face Sledge Hammer", "Estwing Soft Face industrial grade hammer designed to mushroom and not chip when striking hard base materials. Used in construction ,agriculture, automative , mining railroad and maintenance." },
            { "Kincrome 12' 300mm Heavy Duty Hacksaw", "Kincrome's hacksaws are rock solid, no frills, high quality saws that are extremely reliable and tough. Dual spigots allow for 45 degree cutting, built in blade storage and the quick blade change mechanism make blade changes a breeze and the rubberised grip ensures comfort while cutting." },
            { "Spear & Jackson 550mm Hand Saw", "With precision cross-ground teeth for extra sharpness, a high carbon Sheffield steel blade, protective lacquer and high quality solid timber handle, the Spear & Jackson hand saw is a force to be reckoned with." },
            { "NWS 250mm Left Aviation Snips", "The NWS left aviation snips are perfect for short, straight and figure cuts into metal. With a micro-serrated cutting edge to prevent slipping, these tin snips will easily cut without any burrs. The self opening spring and locking feature allows easy use and storage of the tool." },
            { "Craftright 300mm 2 Piece Quick Action Clamp", "The Craftright 300mm Quick Action Clamp holds your materials in place while you work to avoid slipping or unwanted movement. It features a great clamping force, free sliding jaw, one handed quick release trigger for a non-explosive pressure release, non-marring pads which securely grip and protect the finish of your workpiece and a comfortable contoured handle. It is the ideal tool for the home handyman's tool box, to help with home maintenance needs, DIY projects, woodworking and construction jobs." },
            { "Kincrome 8 Piece Mini Plier Set", "KINCROME's range of Soft-Grip Pliers are a great value option for advanced DIY and trade professional users. Each plier is manufactured from forged Chrome Vanadium Steel (Cr-V) with induction hardened & precision machined jaws. Each plier also features TPR injection moulded soft grip handles for increased ergonomics and reduced fatigue." },
            { "Will Tie Wire Cutters", "Durable joint and precision cutting edges for miscellaneous use. Coated handles for comfortable use. Hardened steel pincers." },
            { "Victa 46' 23HP Zero Turn Petrol Ride On Mower", "The Victa ZTX series mower features a 46' (116cm) triple blade cutting deck. Powered by a Briggs and Stratton V twin 23HP engine and with Hydro-Gear ZT-2200 transmissions providing true zero-turn capability; the Victa ZTX zero turn mower will keep large lawns looking manicured all year round." },
            { "Mitsubishi Bronte 8.0kW Reverse Cycle Split System A/C", "Recommended by CHOICE the Bronte 8.0kW split system air conditioner offers a powerful yet quiet heating and cooling solution for larger rooms and spaces. The unit incorporates a range of advanced features and functions, including a powerful 18m airflow, child lock, timers, etc and utilises the new generation R32 refrigerant which increases energy efficiency and minimises running costs." },
        }
        .Select((x, i) => new Product(i + 1, x.Key, x.Value, GetProductAUDPrice(x.Key)))
        .OrderBy(x => x.ProductName)
        .ToList();
    }
}
