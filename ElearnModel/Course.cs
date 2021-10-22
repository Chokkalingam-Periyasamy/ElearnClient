using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Elearn.ElearnModel
{
    public partial class Course
    {
        public Course()
        {
            Usercourses = new HashSet<Usercourse>();
        }

        public int Courseid { get; set; }
        [Required]
        public string Coursename { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]

        public string Amount { get; set; }

        public virtual ICollection<Usercourse> Usercourses { get; set; }
    }
}
