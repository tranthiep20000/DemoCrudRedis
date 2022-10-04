using DemoMVC.Models;
using DemoMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    /// <summary>
    /// Information of CategoryController
    /// CreatedBy: ThiepTT(19/08/2022)
    /// </summary>
    public class CategoryController : Controller
    {
        #region Field

        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly CategoryRepository _categoryRepositoryclass;

        #endregion Field

        #region Contructor

        public CategoryController(IBaseRepository<Category> categoryRepository, CategoryRepository categoryRepositoryclass)
        {
            _categoryRepository = categoryRepository;
            _categoryRepositoryclass = categoryRepositoryclass;
        }

        #endregion Contructor

        #region Method

        public IActionResult Index(string? valueSearch)
        {
            IEnumerable<Category> listCategory;
            if (string.IsNullOrEmpty(valueSearch))
            {
                listCategory = _categoryRepository.GetAll();
            }
            else
            {
                listCategory = _categoryRepositoryclass.GetBySearchValue(valueSearch);
            }
            return View(listCategory);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var category = _categoryRepository.GetById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete((int)id);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

        #endregion Method
    }
}