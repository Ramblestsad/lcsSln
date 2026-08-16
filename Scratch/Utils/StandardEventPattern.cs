using Serilog;

namespace Scratch.Utils;

public class StandardEvent
{
    // subscriber
    public static void Stock_PriceChangedUpTenPercent(object? sender, PriceChangedEventArgs e)
    {
        if (e.LastPrice == 0)
            return;

        var change = ( e.NewPrice - e.LastPrice ) / e.LastPrice;
        if (change > 0.1m)
        {
            var symbol = ( sender as Stock )?.Symbol ?? "?";
            Log.Warning($"Alert, {symbol} rose {change:P0}: {e.LastPrice} -> {e.NewPrice}");
        }
    }

    // convey info
    public class PriceChangedEventArgs(decimal lastPrice, decimal newPrice): EventArgs
    {
        public decimal LastPrice { get; } = lastPrice;
        public decimal NewPrice { get; } = newPrice;
    }

    // broadcaster
    public class Stock(string symbol)
    {
        public string Symbol { get; } = symbol;
        private decimal _price;
        private bool _hasPrice;

        public decimal Price
        {
            get => _price;
            set
            {
                if (_hasPrice && _price == value)
                    return;

                var oldPrice = _price;
                _price = value;

                // First assignment is initialization, not a price change.
                if (!_hasPrice)
                {
                    _hasPrice = true;
                    return;
                }

                OnPriceChanged(new PriceChangedEventArgs(oldPrice, _price));
            }
        }

        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }
}
