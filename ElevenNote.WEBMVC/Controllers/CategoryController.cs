using ElevenNote.Models.CategoryModels;
using ElevenNote.Models.NoteModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WEBMVC.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
        // GET: Category
        //public ActionResult Index()
        //{
        //    var service = CreateCategoryService();
        //    var model = service.GetCategories();
        //    return View(model);
        //}
        public async Task<ActionResult> Index()
        {
            var service = CreateCategoryService();
            var model = await service.GetCategoriesAsync();
            return View(model);
        }

        //Get: Category/Details/{id}
        //public ActionResult Details(int id)
        //{
        //    var svc = CreateCategoryService();
        //    var model = svc.GetCategoryByID(id);

        //    return View(model);
        //}
        public async Task<ActionResult> Details(int id)
        {
            var svc = CreateCategoryService();
            var model = await svc.GetCategoryByIdAsync(id);

            return View(model);
        }

        //Get CAtegory/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post Category/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CategoryCreate category)
        //{
        //    if (!ModelState.IsValid)
        //        return View(category);

        //    var service = CreateCategoryService();

        //    if (service.CreateCategory(category))
        //    {
        //        TempData["SaveData"] = "Your category was created successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Category could not be created.");
        //    return View(category);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var service = CreateCategoryService();

            if (await service.CreateCategoryAsync(category))
            {
                TempData["SaveData"] = "Your category was created successfully.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Category could not be created.");
            return View(category);
        }

        //Get Category/Edt/{id}
        //public ActionResult Edit(int id)
        //{
        //    var service = CreateCategoryService();
        //    var detail = service.GetCategoryByID(id);
        //    var model =
        //        new CategoryEdit
        //        {
        //            CategoryID = detail.CategoryID,
        //            Name = detail.Name
        //        };
        //    return View(model);
        //}
        public async Task<ActionResult> Edit(int id)
        {
            var service = CreateCategoryService();
            var detail = await service.GetCategoryByIdAsync(id);
            var model =
                new CategoryEdit
                {
                    CategoryID = detail.CategoryID,
                    Name = detail.Name
                };
            return View(model);
        }

        //Post Category/Edit/{id}
        //[HttpPost,ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, CategoryEdit category)
        //{
        //    if (!ModelState.IsValid) return View(category);
        //    if (category.CategoryID != id)
        //    {
        //        ModelState.AddModelError("", "ID Mismatch");
        //        return View(category);
        //    }
        //    var service = CreateCategoryService();
        //    if (service.UpdateCategory(category))
        //    {
        //        TempData["SaveResult"] = "Your category was successfully updated.";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", "Your category could not be updated.");
        //    return View(category);
        //}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryEdit category)
        {
            if (!ModelState.IsValid) return View(category);
            if (category.CategoryID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(category);
            }
            var service = CreateCategoryService();
            if (await service.UpdateCategoryAsync(category))
            {
                TempData["SaveResult"] = "Your category was successfully updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your category could not be updated.");
            return View(category);
        }

        //Get Category/Delete/{id}
        //public ActionResult Delete(int id)
        //{
        //    var service = CreateCategoryService();
        //    var detail = service.GetCategoryByID(id);
        //    return View(detail);
        //}
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateCategoryService();
            var detail = await service.GetCategoryByIdAsync(id);
            return View(detail);
        }

        //Post Category/Delete/{id}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteCategory(int id)
        //{
        //    var service = CreateCategoryService();
        //    service.DeleteCategory(id);
        //    TempData["SaveResult"] = "Your category was successfully deleted.";
        //    return RedirectToAction("Index");
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var service = CreateCategoryService();
            if (await service.DeleteCategoryAsync(id))
            {
                TempData["SaveResult"] = "Your category was successfully deleted.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your category could not be updated.");
            return View();
        }

    }
}