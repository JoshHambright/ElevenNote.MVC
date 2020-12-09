using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.NoteModels
{
    public class NoteListItemShort
    {
        [Display(Name = "Note Id")]
        public int NoteID { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        
    }
}
