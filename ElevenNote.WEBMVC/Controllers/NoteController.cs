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
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
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
            {
                return View(note);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            service.CreateNote(note);

            return RedirectToAction("Index");
        }
    }
}