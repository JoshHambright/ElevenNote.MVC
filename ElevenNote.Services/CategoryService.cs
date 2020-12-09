using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using ElevenNote.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userID)
        {
            _userId = userID;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerID = _userId,
                    Name = model.Name,

                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                            .Categories
                            .Where(e => e.OwnerID == _userId)
                            .Select(
                                e =>
                                    new CategoryListItem
                                    {
                                        CategoryID = e.CategoryID,
                                        Name = e.Name,
                                        NumOfNote = e.Notes.Count()
                                    }
                    ).ToList();
                return query;
            }
        }

        public CategoryDetails GetCategoryByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Where(e => e.CategoryID == id && e.OwnerID == _userId)
                        .FirstOrDefault();
                return
                    new CategoryDetails
                    {
                        CategoryID = entity.CategoryID,
                        Name = entity.Name,
                        NumOfNotes = entity.Notes.Count(),
                        Notes = entity.Notes
                                .Select(
                                    x => new NoteListItemShort
                                    {
                                        NoteID = x.NoteID,
                                        Title = x.Title
                                    }
                                ).ToList()
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit category)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Where(e => e.CategoryID == category.CategoryID && e.OwnerID == _userId)
                        .FirstOrDefault();
                entity.Name = category.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Where(e => e.CategoryID == id && e.OwnerID == _userId)
                        .FirstOrDefault();
                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
