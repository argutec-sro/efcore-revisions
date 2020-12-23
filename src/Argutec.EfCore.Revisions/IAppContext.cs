using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Argutec.EfCore.Revisions
{
    public interface IAppContext
    {
        string LoggedUser { get; }
        string LoggedUserName { get; }
    }
}
