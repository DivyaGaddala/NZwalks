
using Microsoft.AspNetCore.Mvc;
using Nzwalk.UI.Models;
using Nzwalk.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace Nzwalk.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpclientfactory;

        public RegionsController(IHttpClientFactory httpclientfactory)
        {
            this.httpclientfactory = httpclientfactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();

            var client = httpclientfactory.CreateClient();

            var httpResponseMessage =
                await client.GetAsync("http://localhost:5065/api/Regions");

            httpResponseMessage.EnsureSuccessStatusCode();

            var regions =
                await httpResponseMessage.Content
                    .ReadFromJsonAsync<IEnumerable<RegionDTO>>();

            if (regions != null)
            {
                response.AddRange(regions);
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel viewmodel)
        {
            var client = httpclientfactory.CreateClient();

            var httpRequestmessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,

                RequestUri = new Uri(
                    "http://localhost:5065/api/Regions"
                ),

                Content = new StringContent(
                    JsonSerializer.Serialize(viewmodel),
                    Encoding.UTF8,
                    "application/json"
                )
            };

            var httpResponsemesage =
                await client.SendAsync(httpRequestmessage);

            if (!httpResponsemesage.IsSuccessStatusCode)
            {
                var error = await httpResponsemesage.Content.ReadAsStringAsync();

                return Content(
                    $"API Error: {httpResponsemesage.StatusCode}<br/>{error}"
                );
            }
            var response =
                await httpResponsemesage.Content
                    .ReadFromJsonAsync<RegionDTO>();

            if (response != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View(viewmodel);
        }


        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {
           var client= httpclientfactory.CreateClient();
            var httpresponse = await client.GetFromJsonAsync<RegionDTO>($"http://localhost:5065/api/regions/{id.ToString()}");
            if(httpresponse is not null)
            {
               return View(httpresponse);
            }
            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult>Edit(RegionDTO request)
        {
            var  client = httpclientfactory.CreateClient();
            var httprequestMessage = new HttpRequestMessage()
            {

                Method = HttpMethod.Put,

                RequestUri = new Uri(
                    "http://localhost:5065/api/Regions/{request.id}"),


                Content = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json"
                )
            };
           var httpResponseMessage= await client.SendAsync(httprequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response =
                   await httpResponseMessage.Content
                       .ReadFromJsonAsync<RegionDTO>();

            if(response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();

        }

        [HttpPost]

        public async Task<IActionResult>Delete(RegionDTO request)
        {
            try
            {
                var client = httpclientfactory.CreateClient();
                var httpresponse = await client.DeleteAsync($"http://localhost:5065/api/Regions/{request.Id}");
                httpresponse.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {

                throw;
            }
            return View("Edit");
        }

    }


}
