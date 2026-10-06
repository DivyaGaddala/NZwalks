
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpclientfactory;

        public RegionsController(IHttpClientFactory httpclientfactory)
        {
            this.httpclientfactory = httpclientfactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpclientfactory.CreateClient();

            var httpResponseMessage =
                await client.GetAsync("https://localhost:7276/api/regions");

            

            var regions =
                await httpResponseMessage.Content
                    .ReadFromJsonAsync<IEnumerable<RegionDTO>>();

            return View(regions);
        }
    }
}
