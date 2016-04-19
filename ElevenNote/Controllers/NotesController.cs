using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Controllers
{
    [Authorize]

    public class NotesController : Controller
    {
        private readonly Lazy<NoteService> _svc;

        public NotesController()
        {
            _svc =
                new Lazy<NoteService>(
                        () =>
                        {
                            var userId = Guid.Parse(User.Identity.GetUserId());
                            return new NoteService(userId);
                        }
                        );
        }
        
        // GET: Notes
        public ActionResult Index()
        {
            var notes = _svc.Value.GetNotes();

            return View(notes);
        }

        public ActionResult Create()
        {
            var vm = new NoteCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateNote(vm))
            {
                ModelState.AddModelError("", "Unable to create note");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var vm = _svc.Value.GetNoteById(id);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _svc.Value.GetNoteById(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(NoteDetailViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.UpdateNote(vm))
            {
                ModelState.AddModelError("", "Unable to update note");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var vm = _svc.Value.GetNoteById(id);

            return View(vm);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _svc.Value.DeleteNote(id);

            return RedirectToAction("Index");
        }
    }
}