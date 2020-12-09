using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        [Display(Name = "Note ID")]
        public int NoteID { get; set; }
        [Required]
        [Display(Name = "Owner ID")]
        public Guid OwnerID { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Note Title")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(8000, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Note")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Date Updated")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        [DefaultValue(false)]
        public bool IsStarred { get; set; }
        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
