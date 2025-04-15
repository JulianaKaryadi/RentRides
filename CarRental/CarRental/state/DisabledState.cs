using CarRental.iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.state
{
    public class DisabledState : UserAccountState
    {
        public override void Enable(UserAccount userAccount)
        {
            userAccount.Enabled = true;
            // No need to set a new state, as it's already handled by the Enabled property
        }

        public override void Disable(UserAccount userAccount)
        {
            // Already disabled
        }
    }
}
