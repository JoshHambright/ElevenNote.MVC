using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ElevenNote.Data;
using ElevenNote.Models.NoteModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;

namespace ElevenNote.WEBMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

        // GET: Note
        //public ActionResult Index()
        //{
        //    var service = CreateNoteService();
        //    var model = service.GetNotes();
        //    return View(model);
        //}
        public async Task<ActionResult> Index()
        {
            var service = CreateNoteService();
            var model = await service.GetNotesAsync();
            return View(model);
        }

        // Get: Note/Details/{id}
        //public ActionResult Details(int id)
        //{

        //    var svc = CreateNoteService();
        //    var model = svc.GetNoteById(id);

        //    return View(model);
        //}

        public async Task<ActionResult> Details(int id)
        {

            var svc = CreateNoteService();
            var model = await svc.GetNoteByIdAsync(id);

            return View(model);
        }


        //Get Note/Create
        //public ActionResult Create()
        //{
        //    var service = CreateNoteService();

        //    ViewBag.CategoryList = new SelectList(_db.Categories, "CategoryID", "Name");

        //    return View();
        //}
        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var catService = new CategoryService(userId);
            var categoryList = await catService.GetCategoriesAsync();

            var catSelectList = categoryList.Select(
                                        e =>
                                            new SelectListItem
                                            {
                                                Value = e.CategoryID.ToString(),
                                                Text = e.Name
                                            }
                                        ).ToList();
                
            return catSelectList;
        }
        public async Task<ActionResult> Create()
        {
            var service = CreateNoteService();
           
            ViewBag.SyncOrAsync = "Asynchronous";
            ViewBag.CategoryID = await GetCategoriesAsync();

            return View();
        }
        //Post Note/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(NoteCreate note)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CategoryList = GetCategoriesAsync();
        //        return View(note);

        //    }

        //    var service = CreateNoteService();

        //    if (service.CreateNote(note))
        //    {
        //        TempData["SaveResult"] = "Your note was created.";
        //        return RedirectToAction("Index");

        //    }

        //    ModelState.AddModelError("", "Note could not be created.");
        //    ViewBag.CategoryList = new SelectList(_db.Categories, "CategoryID", "Name");

        //    return View(note);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoteCreate note)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryID = await GetCategoriesAsync();
                return View(note);

            }

            var service = CreateNoteService();

            if (await service.CreateNoteAsync(note))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "Note could not be created.");
            ViewBag.CategoryID = await GetCategoriesAsync();

            return View(note);
        }


        //Get Note/Edit/{id}
        //public ActionResult Edit(int id)
        //{
        //    var service = CreateNoteService();
        //    var detail = service.GetNoteById(id);
        //    var model =
        //        new NoteEdit
        //        {
        //            NoteID = detail.NoteID,
        //            Title = detail.Title,
        //            Content = detail.Content,
        //            CategoryID = detail.CategoryID
        //        };
        //    ViewBag.CategoryList = new SelectList(_db.Categories, "CategoryID", "Name");

        //    return View(model);
        //}
        public async Task<ActionResult> Edit(int id)
        {
            var service = CreateNoteService();
            var detail = await service.GetNoteByIdAsync(id);
            var model =
                new NoteEdit
                {
                    NoteID = detail.NoteID,
                    Title = detail.Title,
                    Content = detail.Content,
                    CategoryID = detail.CategoryID
                };
            //ViewBag.SyncOrAsync = "Asynchronous";
            ViewBag.CategoryID = await GetCategoriesAsync();

            return View(model);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, NoteEdit note)
        //{
        //    if (!ModelState.IsValid) return View(note);
        //    if(note.NoteID != id)
        //    {
        //        ModelState.AddModelError("", "ID Mismatch");
        //        ViewBag.CategoryList = new SelectList(_db.Categories, "CategoryID", "Name");

        //        return View(note);
        //    }
        //    var service = CreateNoteService();
        //    if (service.UpdateNote(note))
        //    {
        //        TempData["SaveResult"] = "Your note was successfully updated.";
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryList = new SelectList(_db.Categories, "CategoryID", "Name");
        //    ModelState.AddModelError("", "Your note could not be updated.");
        //    return View(note);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, NoteEdit note)
        {
            if (!ModelState.IsValid) return View(note);
            if (note.NoteID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                ViewBag.CategoryID = await GetCategoriesAsync();

                return View(note);
            }
            var service = CreateNoteService();
            if (await service.UpdateNoteAsync(note))
            {
                TempData["SaveResult"] = "Your note was successfully updated.";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = await GetCategoriesAsync();
            ModelState.AddModelError("", "Your note could not be updated.");
            return View(note);
        }

        //Get Note/Delete/{id}
        //public ActionResult Delete(int id)
        //{
        //    var service = CreateNoteService();
        //    var detail = service.GetNoteById(id);
        //    return View(detail);
        //}
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateNoteService();
            var detail = await service.GetNoteByIdAsync(id);
            return View(detail);
        }

        //Post Note/Delete/{id}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletePost(int id)
        //{
        //    var service = CreateNoteService();
        //    service.DeleteNote(id);
        //    TempData["SaveResult"] = "Your note was successfully deleted.";

        //    return RedirectToAction("Index");
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            var service = CreateNoteService();
            await service.DeleteNoteAsync(id);
            TempData["SaveResult"] = "Your note was successfully deleted.";

            return RedirectToAction("Index");
        }
    }
}