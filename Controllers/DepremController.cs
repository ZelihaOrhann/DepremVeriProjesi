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
            string apiUrl = "https://api.orhanaydogdu.com.tr/deprem/";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var depremVerileri = JArray.Parse(response);

            Console.WriteLine(depremVerileri);
            foreach (var item in depremVerileri)
            {
                // Null kontrolü ekleyerek uyarıları gideriyoruz
                var tarih = item["tarih"]?.ToString() ?? "Bilinmiyor";
                var saat = item["saat"]?.ToString() ?? "Bilinmiyor";
                var enlem = item["enlem"]?.ToString() ?? "0";
                var boylam = item["boylam"]?.ToString() ?? "0";
                var derinlik = item["derinlik"]?.ToString() ?? "0";
                var buyukluk = item["buyukluk"]?.ToString() ?? "0";
                var yer = item["yer"]?.ToString() ?? "Bilinmiyor";

                var document = new BsonDocument
                {
                    { "Tarih", tarih },
                    { "Saat", saat },
                    { "Enlem", enlem },
                    { "Boylam", boylam },
                    { "Derinlik", derinlik },
                    { "Buyukluk", buyukluk },
                    { "Yer", yer }
                };

                await _mongoDBService.AddDepremVerisiAsync(document);
            }

            return Ok("Deprem verileri başarıyla kaydedildi.");
    }

    }
}
