using Microsoft.AspNetCore.Mvc;
using Eksamen.Models;
using Eksamen.ViewModels;
using Eksamen.DAL;

using Microsoft.AspNetCore.Authorization;


namespace Eksamen.Controllers;

public class ProductController : Controller
{
    private readonly IItemRepository _matRep;
    private readonly ILogger<ProductController> _logger;


    public ProductController(IItemRepository matRep, ILogger<ProductController> logger)
    {
        _matRep = matRep;
        _logger = logger;
    }

     public async Task<IActionResult> Index()
        {
            var mat = await _matRep.GetAll();
            if (mat == null)
            {
                _logger.LogError("[ProductController] Product list not found while executing _matRep.GetAll()");
                return NotFound("Products list not found");
            }
           var productsViewModel = new ProductsViewModel(mat, "Table");
            return View(productsViewModel);
        }


    public async Task<IActionResult> List(string searchString)
        {
            var mat = from m in await _matRep.GetAll()
                    select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mat = mat.Where(s => s.Name.Contains(searchString));
            }

            if (mat == null)
            {
                _logger.LogError("[ProductController] Product list not found while executing _matRep.GetAll()");
                return NotFound("No products found");
            }
            var productsViewModel = new ProductsViewModel(mat.ToList(), "List");
            return View(productsViewModel);
        }
       
        public async Task<IActionResult> Details(int id)
        {
            var mat = await _matRep.GetItemById(id);

            if (mat == null)
            {
                _logger.LogError("[ProductController] Product list not found for the ProductId {ProductId: 0000}", id);
                return NotFound("Products list not found for the ProductId");
            }
            return View(mat);

        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

         [HttpPost]
        [Authorize]
       
        public async Task<IActionResult> Create(Product mat, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Hvis et bilde er valgt
            if (ImageFile != null) // Sjekker om en fil er lastet opp
                {
                    // Lagrer bildet i wwwroot/images
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Oppdaterer stien til bildet i databasen
                    mat.ImageUpload = "/images/" + fileName;
                }
                // Lagre product (mat) i databasen ved å bruke repository-mønsteret
                bool ok = await _matRep.Create(mat);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // Hvis noe går galt, logg advarselen og returner skjemaet på nytt
            _logger.LogWarning("[ProductController] Product creation failed {@product}", mat);
            return View(mat);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var mat = await _matRep.GetItemById(id);
            if (mat == null)
            {
                _logger.LogError("[ProductController] Product list not found for the ProductId {ProductId: 0000}", id);
                return NotFound("Products list not found for the ProductId");
            }
            return View(mat);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Product mat, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                  // Hvis et nytt bilde er valgt
                if (ImageFile != null) // Sjekker om en fil er lastet opp
                {
                    // Lagrer bildet i wwwroot/images
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Oppdaterer stien til bildet i databasen
                    mat.ImageUpload = "/images/" + fileName;
                }

               bool ok = await _matRep.Update(mat);
                if(ok)
                {
                     return RedirectToAction(nameof(List));
                }
                   
            }
             _logger.LogWarning("[ProductController] Product creation failed {@product}", mat);
            return View(mat);
        }

         [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var mat = await _matRep.GetItemById(id);
            if (mat == null)
            {
                _logger.LogError("[ProductController] Product list not found for the ProductId {ProductId: 0000}", id);
                return NotFound("Products list not found for the ProductId");
            }
            return View(mat);
        }
        

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool ok = await _matRep.Delete(id);
            if (!ok)
            {
                _logger.LogWarning("[ProductController] Product deletion failed for the ProductId {ProductId: 0000}", id);
                return BadRequest("Product deletion failed");
            }
                
            return RedirectToAction(nameof(List));

        }


}
        
