using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Elearn.ElearnModel
{
    public partial class Module
    {
        public int Moduleid { get; set; }
        [Required]
        public int? Courseid { get; set; }
        [Required]
        public string Modulename { get; set; }
        [Required]
        public string Video { get; set; }
    }
}
