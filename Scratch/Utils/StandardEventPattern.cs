using Serilog;

namespace Scratch.Utils;

public class StandardEvent
{
    // subscriber
    public static void Stock_PriceChangedUpTenPercent(object? sender, PriceChangedEventArgs e)
    {
        if (( e.NewPrice - e.LastPrice ) / e.LastPrice > 0.1M)
            Log.Warning("Alert, 10% stock price increase!");
    }

    // convey info
    public class PriceChangedEventArgs(decimal lastPrice, decimal newPrice): EventArgs
    {
        public readonly decimal LastPrice = lastPrice;
        public readonly decimal NewPrice = newPrice;
    }

    // broadcaster
    public class Stock(string symbol)
    {
        private readonly string _symbol = symbol;
        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price == value) return;
                var oldPrice = _price;
                _price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, _price));
            }
        }

        // broadcaster
        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }
}
