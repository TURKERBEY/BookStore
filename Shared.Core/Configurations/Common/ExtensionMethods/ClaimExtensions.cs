using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.ExtensionMethods
{
    public static class ClaimExtensions
    {
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddUserName(this ICollection<Claim> claims, string username)
        {
            claims.Add(new Claim("UserName", username));
        }
    }
}
