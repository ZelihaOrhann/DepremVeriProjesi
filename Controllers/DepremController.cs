using Microsoft.AspNetCore.Mvc;
using DepremVeriProjesi.Services;
using MongoDB.Bson;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DepremVeriProjesi.Controllers
{
    public class DepremController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        private readonly HttpClient _httpClient;

        public DepremController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://deprem.afad.gov.tr/api/sondepremler";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var depremVerileri = JArray.Parse(response);

            foreach (var item in depremVerileri)
            {
                var document = new BsonDocument
                {
                    { "Tarih", item["tarih"].ToString() },
                    { "Saat", item["saat"].ToString() },
                    { "Enlem", item["enlem"].ToString() },
                    { "Boylam", item["boylam"].ToString() },
                    { "Derinlik", item["derinlik"].ToString() },
                    { "Buyukluk", item["buyukluk"].ToString() },
                    { "Yer", item["yer"].ToString() }
                };
                await _mongoDBService.AddDepremVerisiAsync(document);
            }

            return View(depremVerileri);
        }
    }
}
