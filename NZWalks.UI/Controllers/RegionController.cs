using Microsoft.AspNetCore.Mvc;

namespace NZWalks.UI.Controllers
{
    public class RegionController
    {
        private readonly IHttpClientFactory httpClientFactory;
        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7190/api/regions");
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Response = stringResponse;
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
