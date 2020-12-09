﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryCreate
    {
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
