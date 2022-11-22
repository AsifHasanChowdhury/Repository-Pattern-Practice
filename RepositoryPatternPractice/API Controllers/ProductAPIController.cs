using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryPatternPractice.Models;
using RepositoryPatternPractice.Models.Data_Access_Layer;
using RepositoryPatternPractice.Models.Data_Access_Layer.Class;
using System.Security.Policy;

namespace RepositoryPatternPractice.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {

        private IProductRepository _productRepository;

        // Uri baseAddress = new Uri("http://localhost:44396/productApi");
        //HttpClient _httpClient;    
        
        public ProductAPIController(IConfiguration configuration)
        {
            _productRepository = new ProductRepository(configuration);
           // _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        [Route("AllProduct")]
        public string Index()
        {
            List<Product_Table> productList = new List<Product_Table>();
            string json = "";
           
                
                json = JsonConvert.SerializeObject(_productRepository.GetProducts());

            //HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_httpClient.BaseAddress+"/AllProduct").Result;
            //if (httpResponseMessage.IsSuccessStatusCode)
            //{
            //    string data=httpResponseMessage.Content.ReadAsStringAsync().Result;
            //productList=JsonConvert.DeserializeObject
            //}
            return json;

        }
    }
}
