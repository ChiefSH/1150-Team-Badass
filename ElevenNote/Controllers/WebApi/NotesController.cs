using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.Controllers.WebApi
{
    [Authorize]
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
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

        [Route("{id}")]
        public IEnumerable<NoteListItemViewModel> Get()
        {
            return _svc.Value.GetNotes();
        }

        [Route]
        public NoteDetailViewModel Get(int id)
        {
            return _svc.Value.GetNoteById(id);
        }

        [Route]
        public bool Post(NoteCreateViewModel vm)
        {
            return _svc.Value.CreateNote(vm);
        }

        [Route("{id}")]
        public bool Pust(int id, NoteDetailViewModel vm)
    {
        return _svc.Value.UpdateNote(vm);
    }

        [Route("{id}")]
        public bool Delete(int id)
        {
        return _svc.Value.DeleteNote(id);
        }

        [Route("{id}/Star")]
        [HttpPost]

        public bool ToggleStarOn(int id) => SetStarState(id, true);
        

        private bool SetStarState(int noteId, bool state)
        {
            var note =
                _svc.Value.GetNoteById(noteId);

            note.IsStarred = state;

            return _svc.Value.UpdateNote(note);
        }

        [Route ("{id}/Star")]
        [HttpDelete]

        public bool ToggleStarOff(int id)=>   SetStarState(id, false);
        

    }
}
