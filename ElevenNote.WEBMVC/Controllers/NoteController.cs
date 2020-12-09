using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElevenNote.Models.NoteModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;

namespace ElevenNote.WEBMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

        // GET: Note
        public ActionResult Index()
        {
            var service = CreateNoteService();
            var model = service.GetNotes();
            return View(model);
        }

        // Get: Note/Details/{id}
        public ActionResult Details(int id)
        {

            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }



        //Get Note/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return View(note);

            var service = CreateNoteService();

            if (service.CreateNote(note))
            {
                TempData["SaveResult"] = "You note was created.";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "Note could not be created.");
            return View(note);

        }

        //Get Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteID = detail.NoteID,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit note)
        {
            if (!ModelState.IsValid) return View(note);
            if(note.NoteID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(note);
            }
            var service = CreateNoteService();
            if (service.UpdateNote(note))
            {
                TempData["SaveResult"] = "Your note was successfully updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(note);
        }

        //Get Note/Delete/{id}
        public ActionResult Delete(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            return View(detail);
        }

        //Post Note/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();
            service.DeleteNote(id);
            TempData["SaveResult"] = "Your note was successfully deleted.";

            return RedirectToAction("Index");
        }
    }
}