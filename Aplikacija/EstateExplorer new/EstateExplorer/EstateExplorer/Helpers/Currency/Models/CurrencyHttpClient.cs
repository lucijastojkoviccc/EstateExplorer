using Newtonsoft.Json;

namespace EstateExplorer.Helpers.Currency.Models
{
    public class CurrencyHttpClient
    {
        private readonly HttpClient _httpClient;

        public CurrencyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrencyResponse> GetCurrency(string currency)
        {
            var currencyResponse = await _httpClient.GetAsync($"https://kurs.resenje.org/api/v1/currencies/{currency}/rates/today");

            if (!currencyResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var currencyStringResponse = await currencyResponse.Content.ReadAsStringAsync();
            CurrencyResponse response = JsonConvert.DeserializeObject<CurrencyResponse>(currencyStringResponse);

            return response;
        }
    }
}
