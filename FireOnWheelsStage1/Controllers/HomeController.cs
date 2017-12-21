using System.Web.Mvc;
using FireOnWheelsStage1.Helper;
using FireOnWheelsStage1.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace FireOnWheelsStage1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            order.Price = PriceCalculator.GetPrice(order);
            return View("Review", order);
        }

        public async Task<ActionResult> Confirm(Order order)
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["serviceBaseAddress"]);
            var response = await client.PostAsJsonAsync("Dispatch", order);
            response.EnsureSuccessStatusCode();
            //EmailSender.SendEmailToDispatch(order);
            return View();
        }
    }
}
