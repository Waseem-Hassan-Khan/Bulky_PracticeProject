using BulkyPractice.DataAcess.Data;
using BulkyPractice.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulky_PracticeProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext appDbContext) {
           _context = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> list = await _context.Categories.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(category.Name.ToString() == category.DisplayOrder.ToString())
            {
                /*
                 * 1. asp-for tag is used to associate a form input element with a model property. It
                 * helps with model binding and validation. When the form is submitted, the value entered
                 * in the input field associated with asp-for is bound to the corresponding property in the model.
                 * 
                 * 2. The framework is case-insensitive while looking for error so if even you provide
                 * "name" or "Name" as long as spelling is correct it should work fine.
                 * 
                 * 3. If we do not pass the property name e.g ("name" or "Name"), it would display the error only
                 * in global summary.
                 */
                ModelState.AddModelError("name" , "Name and Display Order can not be same.");
            }
            if(ModelState.IsValid)
            {

                await _context.Categories.AddAsync(category);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            TempData["error"] = "Error creating category";
            return View();
        }

        /*
         * 1. The "asp-route" attribute is used for property-based routing.
         * 
         * 2. You can use any name with "asp-route-", and this will be used as a query parameter in the URL. e.g ("asp-route-Id = "@obj.Id" 
         *     or asp-route-CategoryId = "@obj.Id"  or asp-route-Name = "@obj.Id")
         *     
         * 3. The values for these parameters are specified using the "@model.Property" syntax or in my case "@obj.Property"
         * 
         * 4. The parameter names in "asp-route" should match the parameter names expected by the corresponding action method in the controller.
         *      e.g (asp-route-CatId = "@obj.Id" function(int CatId))
         */

        public async Task<IActionResult> EditCategory(int? id)
        {
            if(id == null || id == 0)
                return NotFound();
            /*
             * The "Find()" method directly works on the primary key so when id is provided
             * it looks for the primary key in the model and returns the row based on the key
             */
            Category obj = _context.Categories.Find(id);
            if(obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (category.Name.ToString() == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display Order can not be same.");
            }

            if (ModelState.IsValid)
            {
                 _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
           
            Category model = _context.Categories.Find(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost ,ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryPost(int? id)
        {
            Category? obj = _context.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
                _context.Categories.Remove(obj);
                _context.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");

        }
    }
}
