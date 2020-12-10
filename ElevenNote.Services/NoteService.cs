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

        public async Task<bool> CreateNoteAsync(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    CategoryId = model.CategoryID
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }
        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    CategoryId = model.CategoryID
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public async Task<IEnumerable<NoteListItem>> GetNotesAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await
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
                                    CreatedUtc = e.CreatedUtc,
                                    CategoryName = e.Category.Name
                                }
                        ).ToListAsync();
                return query.OrderBy(e => e.NoteID);
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
                                    CreatedUtc = e.CreatedUtc,
                                    CategoryName = e.Category.Name
                                }
                        );
                return query.ToList().OrderBy(e => e.NoteID);
            }
        }
        public async Task<NoteDetail> GetNoteByIdAsync(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity = await
                    ctx
                        .Notes
                        .Where(e => e.NoteID == id && e.OwnerID == _userId)
                        .FirstOrDefaultAsync();
                return
                    new NoteDetail
                    {
                        NoteID = entity.NoteID,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        IsStarred = entity.IsStarred,
                        CategoryID = entity.CategoryId,
                        CategoryName = entity.Category.Name
                    };
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
                        ModifiedUtc = entity.ModifiedUtc,
                        IsStarred = entity.IsStarred,
                        CategoryID = entity.CategoryId,
                        CategoryName = entity.Category.Name
                    };
            }
        }
        public async Task<bool> UpdateNoteAsync(NoteEdit note)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                        .Notes
                        .Where(e => e.NoteID == note.NoteID && e.OwnerID == _userId)
                        .FirstOrDefaultAsync();
                entity.Title = note.Title;
                entity.Content = note.Content;
                entity.ModifiedUtc = DateTimeOffset.Now;
                entity.IsStarred = note.IsStarred;
                entity.CategoryId = note.CategoryID;

                //ctx.Entry(entity).State = EntityState.Modified;
                return await ctx.SaveChangesAsync() == 1;
            }
        }
        public bool UpdateNote(NoteEdit note)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteID == note.NoteID && e.OwnerID == _userId);
                entity.Title = note.Title;
                entity.Content = note.Content;
                entity.ModifiedUtc = DateTimeOffset.Now;
                entity.IsStarred = note.IsStarred;
                entity.CategoryId = note.CategoryID;

                //ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges() == 1;
            }
        }
        public async Task<bool> DeleteNoteAsync(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                        .Notes
                        .Where(e => e.NoteID == id && e.OwnerID == _userId)
                        .FirstOrDefaultAsync();

                ctx.Notes.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
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
