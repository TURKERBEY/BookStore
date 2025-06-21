using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.Sessions;
public static class Session
{
    private static readonly AsyncLocal<int?> _userId = new();
    private static readonly AsyncLocal<string?> _userName = new();

    public static int? UserId => _userId.Value;
    public static string? UserName => _userName.Value;

    public static void SetUser(ClaimsPrincipal user)
    {
        _userId.Value = Convert.ToInt32(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        _userName.Value = user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static void Clear()
    {
        _userId.Value = null;
        _userName.Value = null;
    }
}