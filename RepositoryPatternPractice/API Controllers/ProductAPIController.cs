using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryPatternPractice.API_Repository;
using RepositoryPatternPractice.Models;
using RepositoryPatternPractice.Models.Data_Access_Layer;
using RepositoryPatternPractice.Models.Data_Access_Layer.Class;
using RepositoryPatternPractice.Static_Details;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Helpers;

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
        static String dynamicObject = "";

        [HttpGet]
        [Route("AllProduct")]
        public string Index()
        {
            //List<Product_Table> productList = new List<Product_Table>();
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


        [HttpGet]
        [Route("allproductfield")]
        public string getproductfieldfromdb()
        {

            var json = JsonConvert.SerializeObject(_productRepository.GetFormField());
            return json;
        }



        [HttpPost]
        [Route ("StoreProduct")]
        public void CreateProduct([FromBody] Object json)
        {


            var  dynamicS = JsonConvert.DeserializeObject<Object>(json.ToString());
            dynamicObject = dynamicS.ToString();
            var keyValuePairs = JObject.Parse(dynamicObject.ToString());



            //foreach(var item in StaticDetails.DbcolumList)
            //{
            //    //var value= keyValuePairs.Value(item);
            //    //var value= keyValuePairs[item];
            //    //var value = keyValuePairs.GetValue(item);
            //    //var value = keyValuePairs.SelectToken(item);
            //    var value = keyValuePairs.Value<String>(item);
            //    //Debug.WriteLine(value);

            //}

        }

        [HttpGet]
        [Route("HtmlForm")]

        public string HtmlReport()
        {
            return dynamicObject.ToString();    
        }











            [HttpGet]
        [Route("LogReport")]

        public string LogReport()
        {
            
            LogReport logReport = new LogReport();
            
            //var json = JsonConvert.SerializeObject(logReport.ReadLog());
            //return json;
            //return null;

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(logReport.ReadLog(), jsonSerializerSettings);
            return jsonStr;
        }




    }


}
