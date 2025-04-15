using CarRental.iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.state
{
    public class EnabledState : UserAccountState
    {
        public override void Enable(UserAccount userAccount)
        {
            // Already enabled
        }

        public override void Disable(UserAccount userAccount)
        {
            userAccount.Enabled = false;
            // No need to set a new state, as it's already handled by the Enabled property
        }
    }
}
