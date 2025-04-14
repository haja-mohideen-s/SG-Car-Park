using System.Text;
using CarParkSG.Models;
using CarParkSG.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarParkSG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public HomeController(ILogger<HomeController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// Gathers the HDB locations and car park availability seperately and join them and pass it to the view
        /// </summary>
        /// <returns>Car Park availability data</returns>
        public async Task<IActionResult> Index()
        {
            CarParkDataModelView cp = new CarParkDataModelView();
            try
            {
                HDBCarParkModel carParkInfo = await GetHdbCarParksAsync();
                CarParkAvailabilityModel parkingSlotsInfo = await GetCarParkAvailabilityAsync();
                cp = carParkInfo.FillSlotsInfo(parkingSlotsInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading index");
            }
            return View(model: cp);
        }

        private async Task<HDBCarParkModel> GetHdbCarParksAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientExtensions.HdbClient);

            HDBCarParkModel hdbCarParks = new();
            HttpRequestMessage carParkRequest = new HttpRequestMessage(HttpMethod.Get, 
                    $"datastore_search?resource_id={_configuration["HdbResourceId"]}&limit=5000");

            using (var response = await client.SendAsync(carParkRequest))
            {
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    hdbCarParks = JsonConvert.DeserializeObject<HDBCarParkModel>(apiResponse)!;
                }
            }

            return hdbCarParks;
        }

        private async Task<CarParkAvailabilityModel> GetCarParkAvailabilityAsync()
        {
            CarParkAvailabilityModel availabilityInfo = new();
            HttpClient client = _httpClientFactory.CreateClient(HttpClientExtensions.CarParkClient);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get,
                $"carpark-availability?date_time={DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}");

            using (var response = await client.SendAsync(message))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    availabilityInfo = JsonConvert.DeserializeObject<CarParkAvailabilityModel>(apiResponse)!;
                }
            }
            return availabilityInfo;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}