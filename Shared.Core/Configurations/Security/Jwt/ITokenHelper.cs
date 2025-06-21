using Shared.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Security.Jwt;
  public interface ITokenHelper
{
    AccessToken CreateToken(Kullanici user);
}