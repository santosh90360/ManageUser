using Microsoft.AspNetCore.Mvc;
using ManageUser.Models;
using Newtonsoft.Json;
using System.Net;
using Microsoft.DotNet.MSIdentity.Shared;

namespace ManageUser.Controllers
{
    public class ManageGridController : Controller
    {
        public IActionResult Index()
        {
            //Fetch the JSON string from URL.
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = (new WebClient()).DownloadString("https://ag-grid.com/example-assets/row-data.json");
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json); //Deserialize Object     
            return View(products);
        }
    }
}
