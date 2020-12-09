using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.NoteModels
{
    public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage ="Please enter a title of at least 2 characters")]
        [MaxLength(150, ErrorMessage ="There are too many characters in this field.")]
        public string Title { get; set; }
        [MaxLength(8000, ErrorMessage ="Note Content exceeds maximum length, please limit notes to 8000 characters")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
