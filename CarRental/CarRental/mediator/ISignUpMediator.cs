using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.mediator
{
    public interface ISignUpMediator
    {
        void RegisterUser(string username, string password, string email, string phone, string role);
    }
}

