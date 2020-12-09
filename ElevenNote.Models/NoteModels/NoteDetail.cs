using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.NoteModels
{
    public class NoteDetail
    {
        [Display(Name ="Note ID")]
        public int NoteID { get; set; }
        [Display(Name = "Note Title")]
        public string Title { get; set; }
        [Display(Name = "Note")]
        public string Content { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Updated")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        [UIHint("Starred")]
        public bool IsStarred { get; set; }

    }
}
