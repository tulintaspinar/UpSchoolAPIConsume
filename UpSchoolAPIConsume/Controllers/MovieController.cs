using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UpSchoolAPIConsume.Models;
using Newtonsoft.Json;

namespace UpSchoolAPIConsume.Controllers
{
    public class MovieController : Controller
    {
        List<MovieListModel> movies= new List<MovieListModel>();
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                        { "X-RapidAPI-Key", "9c490cca9cmsh32e02dc8372607bp13fecfjsn68fbcb214812" },
                        { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<MovieListModel>>(body);
                return View(movies);
            }
        }
    }
}
