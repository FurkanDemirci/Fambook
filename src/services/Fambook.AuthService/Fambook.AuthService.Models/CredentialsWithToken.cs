using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.AuthService.Models
{
    public class CredentialsWithToken
    {
        public Credentials Credentials { get; set; }
        public string Token { get; set; }
    }
}
