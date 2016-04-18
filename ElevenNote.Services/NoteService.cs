using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<NoteListItemViewModel> GetNotes()
        {
            using (var context = new ElevenNoteDbContext())
            {
                return
                    context
                            .Notes
                            .Where(e => e.OwnerId == _userId)
                            .Select(
                                e =>
                                    new NoteListItemViewModel
                                    {
                                        NoteId = e.NoteId,
                                        Title = e.Title,
                                        IsStarred = e.IsStarred,
                                        CreatedUtc = e.CreatedUtc
                                    })
                            .ToArray();
            }
        }

        public bool CreateNote(NoteCreateViewModel vm)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var entity =
                    new NoteEntity
                    {
                        OwnerId = _userId,
                        Title = vm.Title,
                        Content = vm.Content,
                        CreatedUtc = DateTimeOffset.UtcNow
                    };



                ctx.Notes.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public NoteDetailViewModel GetNoteById(int noteId)
        {
            NoteEntity entity;

            using(var ctx = new ElevenNoteDbContext())
            {
                entity =
                    ctx
                        .Notes
                        .Single(e => e.OwnerId == _userId && e.NoteId == noteId);
            }

            return
                new NoteDetailViewModel
                {
                    NoteId = entity.NoteId,
                    Title = entity.Title,
                    Content = entity.Content,
                    IsStarred = entity.IsStarred,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
        }

        public bool UpdateNote(NoteDetailViewModel vm)
        {
            using(var ctx = new ElevenNoteDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.OwnerId == _userId && e.NoteId == vm.NoteId);

                entity.Title = vm.Title;
                entity.Content = vm.Content;
                entity.IsStarred = vm.IsStarred;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ElevenNoteDbContext())
            {
                var entity =
                            ctx
                                 .Notes
                                 .Single(e => e.OwnerId == _userId && e.NoteId == noteId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
