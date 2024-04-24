using BlankSolution.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlankSolution.MVC.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7266/api");
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            List<GenreGetListViewModel> list = new List<GenreGetListViewModel>();
            var responseMessage = await _httpClient.GetAsync(baseAddress + "/genres/getall");
            if(responseMessage.IsSuccessStatusCode)
            {
                var datas = await responseMessage.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<GenreGetListViewModel>>(datas);

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateViewModel model)
        {
            using(var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(model.Name), "Name");

                if(model.ImageFile != null) 
                {
                    byte[] data;

                    using(var br= new BinaryReader(model.ImageFile.OpenReadStream())) 
                    {
                        data = br.ReadBytes((int)model.ImageFile.Length);

                        ByteArrayContent bytes = new ByteArrayContent(data);
                        bytes.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.ImageFile.ContentType);

                        content.Add(bytes, "ImageFile", model.ImageFile.FileName);
                    }
                }
                var response = await _httpClient.PostAsync(baseAddress + "/genres/post" , content);
                if(response.IsSuccessStatusCode) 
                {
                    return RedirectToAction("Index");
                }
                return View();
            }

        }



    }
}
