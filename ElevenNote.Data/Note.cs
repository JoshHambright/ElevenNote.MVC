﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(2000, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Note")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Date Updated")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
