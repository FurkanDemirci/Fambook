using System.Collections.Generic;
using System.Linq;
using Fambook.AuthService.Models;

namespace Fambook.AuthService.Logic.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Credentials> WithoutPasswords(this IEnumerable<Credentials> credential)
        {
            return credential.Select(x => x.WithoutPassword());
        }

        public static Credentials WithoutPassword(this Credentials credential)
        {
            credential.Password = null;
            return credential;
        }
    }
}
