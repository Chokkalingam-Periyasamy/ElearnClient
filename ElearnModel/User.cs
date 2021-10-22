using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Elearn.ElearnModel
{
    public partial class User
    {
        public User()
        {
            Usercourses = new HashSet<Usercourse>();
        }

        public int Userid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Image { get; set; }

        public virtual ICollection<Usercourse> Usercourses { get; set; }
    }
}
