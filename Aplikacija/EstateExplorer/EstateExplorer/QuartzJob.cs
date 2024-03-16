using Quartz;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using HtmlAgilityPack;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using EstateExplorer.Helpers.Currency.Models;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Microsoft.EntityFrameworkCore;

namespace EstateExplorer
{
    [DisallowConcurrentExecution]
    public class QuartzJob : IJob
    {
        private ApplicationDbContext _context { get; set; }
        private readonly CurrencyHttpClient _currencyHttpClient;
        private readonly ILogger<QuartzJob> _logger;
        public QuartzJob(CurrencyHttpClient currencyHttpClient, ILogger<QuartzJob> logger, ApplicationDbContext context)
        {
            _currencyHttpClient = currencyHttpClient;
            _logger = logger;
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string currencyCode = "eur";
            var currency = await _currencyHttpClient.GetCurrency(currencyCode);
            if (currency is not null)
            {
                var existingCurrency = await _context.CurrencyValues.Where(cv => cv.Code.ToLower() == currencyCode).FirstOrDefaultAsync();
                if (existingCurrency == null)
                {
                    var currencyValues = new CurrencyValues
                    {

                        Code = currency.Code,
                        ExchangeMiddle = currency.ExchangeMiddle,
                        CashBuy = currency.CashBuy,
                        CashSell = currency.CashSell
                    };
                    _context.CurrencyValues.Add(currencyValues);
                }
                else
                {
                    existingCurrency.ExchangeMiddle = currency.ExchangeMiddle;
                    existingCurrency.CashBuy = currency.CashBuy;
                    existingCurrency.CashSell = currency.CashSell;

                    _context.CurrencyValues.Update(existingCurrency);
                }
                
                await _context.SaveChangesAsync();

            }
        }
    }
}
