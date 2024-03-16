using EstateExplorer.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EstateExplorer.Controllers
{
    //public class OidcConfigurationController : Controller
    //{
    //    private readonly ILogger<OidcConfigurationController> _logger;

    //    public OidcConfigurationController(
    //        IClientRequestParametersProvider clientRequestParametersProvider,
    //        ILogger<OidcConfigurationController> logger)
    //    {
    //        ClientRequestParametersProvider = clientRequestParametersProvider;
    //        _logger = logger;
    //    }

    //    public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

    //    [HttpGet("_configuration/{clientId}")]
    //    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    //    {
    //        var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
    //        return Ok(parameters);
    //    }
    //}
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(
            ILogger<OidcConfigurationController> logger)
        {

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}