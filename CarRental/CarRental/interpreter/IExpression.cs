using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.interpreter
{
    public interface IExpression
    {
        void Interpret(SqlCommand cmd);
    }
}

