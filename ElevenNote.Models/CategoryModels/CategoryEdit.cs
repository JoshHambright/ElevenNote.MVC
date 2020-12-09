using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryEdit
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
