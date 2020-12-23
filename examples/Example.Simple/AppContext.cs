using System;
using System.Threading;
using Argutec.EfCore.Revisions;
using Microsoft.EntityFrameworkCore;

namespace Example.Simple
{
    public class AppContext : IAppContext
    {
        public string LoggedUserName => "Logged USER NAME";

        public string LoggedUser => mLoggedUserID.ToString();

        private Guid mLoggedUserID = Guid.NewGuid();
    }
}
