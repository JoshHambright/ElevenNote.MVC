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
    }
}