using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argos.Models.Interfaces
{
    public interface ISecurityContext
    {
        string UserName { get; }

        List<string> UserRoles { get; }
    }
}
