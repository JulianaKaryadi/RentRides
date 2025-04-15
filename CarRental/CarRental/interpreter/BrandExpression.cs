using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.interpreter
{
    public class BrandExpression : IExpression
    {
        private string brand;
        public BrandExpression(string brand)
        {
            this.brand = brand;
        }

        public void Interpret(SqlCommand cmd)
        {
            if (!string.IsNullOrEmpty(brand))
            {
                cmd.CommandText += " AND Brand = @Brand";
                cmd.Parameters.AddWithValue("@Brand", brand);
            }
        }
    }
}

