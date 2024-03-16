using System.ComponentModel.DataAnnotations;

namespace EstateExplorer.Models
{
    public class CurrencyValues
    {
        [Key]
        public int id { get; set; }

        public string Code { get; set; }

        public double ExchangeMiddle { get; set; }

        public double CashBuy { get; set; }

        public double CashSell { get; set; }
    }
}
