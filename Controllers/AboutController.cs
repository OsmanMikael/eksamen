using Microsoft.AspNetCore.Mvc;
using MatRegistrering.Models;

namespace MatRegistrering.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            var viewModel = new AboutViewModel
            {
                CompanyName = "MatRegistrering",
                Description = "Vi tilbyr en enkel og effektiv løsning for matregistrering, inkludert ernæringsinformasjon.",
                Mission = "Vår misjon er å gjøre matregistrering enklere og mer tilgjengelig for alle.",
                Vision = "Å bli den ledende plattformen for mat- og ernæringsregistrering globalt."
            };

            return View(viewModel);
        }
    }
}
