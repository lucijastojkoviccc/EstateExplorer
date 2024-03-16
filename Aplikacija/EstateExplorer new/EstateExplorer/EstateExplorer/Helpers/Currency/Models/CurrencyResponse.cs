using Newtonsoft.Json;

namespace EstateExplorer.Helpers.Currency.Models
{
    public class CurrencyResponse
    {
        [JsonProperty("cash_buy")]
        public double CashBuy { get; set; }

        [JsonProperty("cash_sell")]
        public double CashSell { get; set; }

        [JsonProperty("exchange_middle")]
        public double ExchangeMiddle { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

    }
}
