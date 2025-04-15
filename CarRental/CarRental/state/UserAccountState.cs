using CarRental.iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.state
{
    public abstract class UserAccountState
    {
        public abstract void Enable(UserAccount userAccount);
        public abstract void Disable(UserAccount userAccount);
    }
}
