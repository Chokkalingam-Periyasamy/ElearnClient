using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Elearn.ElearnModel
{
    public partial class staff
    {

        public int Staffid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Expertise { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
