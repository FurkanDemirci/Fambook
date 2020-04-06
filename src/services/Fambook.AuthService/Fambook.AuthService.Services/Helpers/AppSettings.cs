using System;
using System.Collections.Generic;
using System.Text;

namespace Fambook.AuthService.Services.Helpers
{
    public class AppSettings
    {
        public string Secret { get; }

        public AppSettings()
        {
            Secret = "THIS IS A TEST KEY USED BY FAMBOOK. PLEASE CHANGE THIS TO SOMETHING USEFULL AND HIDE IT AWAY SO ITS NOT IN CODE";
        }
    }
}
