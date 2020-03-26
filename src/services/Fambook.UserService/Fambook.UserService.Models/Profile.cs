using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fambook.UserService.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        public string Gender { get; set; }
        
        public string ProfilePicture { get; set; }
        
        public string Description { get; set; }
    }
}
