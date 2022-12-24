using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UpSchoolAPIConsume.Models;

namespace UpSchoolAPIConsume.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CategoryController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync("https://localhost:44375/api/Category");
			if(response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jsonData);
				return View(result);
			}
			else
			{
				ViewBag.responseError = "Suncuya bağlanırken hata oluştu.";
                return View();
            }
			
		}
		[HttpGet]
		public IActionResult AddCategory()
		{
            List<CategoryViewModel> categories = new List<CategoryViewModel>
            {
                new CategoryViewModel { Status=true },
                new CategoryViewModel {Status=false }
            };
            ViewBag.categories = categories;
            return View();
		}
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel category)
        {
            var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(category);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44375/api/Category", content);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
            else
            {
                ViewBag.responseError = "Suncuya bağlanırken hata oluştu.";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44375/api/Category/"+id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CategoryViewModel>(jsonData);
                return View(result);
            }
            else
            {
                ViewBag.responseError = "Suncuya bağlanırken hata oluştu.";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel category)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(category);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44375/api/Category", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.responseError = "Suncuya bağlanırken hata oluştu.";
                return View();
            }
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:44375/api/Category/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
