using ElevenNote.Data;
using ElevenNote.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userID)
        {
            _userId = userID;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerID == _userId)
                        .Select(
                            e =>
                                new NoteListItem
                                {
                                    NoteID = e.NoteID,
                                    Title = e.Title,
                                    IsStarred = e.IsStarred,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToList().OrderBy(e => e.NoteID);
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == id && e.OwnerID == _userId);
                return
                    new NoteDetail
                    {
                        NoteID = entity.NoteID,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateNote(NoteEdit note)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == note.NoteId && e.OwnerID == _userId);
                entity.Title = note.Title;
                entity.Content = note.Content;
                entity.ModifiedUtc = DateTimeOffset.Now;
                ctx.Entry(entity).State = EntityState.Modified;
                
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == id && e.OwnerID == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
