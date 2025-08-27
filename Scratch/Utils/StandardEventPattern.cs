using Serilog;

namespace Scratch.Utils;

public class StandardEvent {
    // subscriber
    public static void Stock_PriceChangedUpTenPercent(object? sender, PriceChangedEventArgs e) {
        if (( e.NewPrice - e.LastPrice ) / e.LastPrice > 0.1M)
            Log.Warning("Alert, 10% stock price increase!");
    }

    // convey info.
    public class PriceChangedEventArgs: EventArgs {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice) {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }

    // broadcaster
    public class Stock {
        private readonly string _symbol;
        private decimal _price;

        public Stock(string symbol) => this._symbol = symbol;

        public decimal Price {
            get => _price;
            set {
                if (_price == value) return;
                var oldPrice = _price;
                _price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, _price));
            }
        }

        // broadcaster
        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e) {
            PriceChanged?.Invoke(this, e);
        }
    }
}
