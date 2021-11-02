﻿using System;
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

        [Required(ErrorMessage = "Phone Number is reqired")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number must be 10 numbers")]
        [RegularExpression(@"^\(?([7-9][0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
