using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.AuthService.Models
{
    public class UserWithToken
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
