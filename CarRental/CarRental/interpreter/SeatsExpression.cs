using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.interpreter
{
    public class SeatsExpression : IExpression
    {
        private int? seats;
        public SeatsExpression(int? seats)
        {
            this.seats = seats;
        }

        public void Interpret(SqlCommand cmd)
        {
            if (seats.HasValue)
            {
                cmd.CommandText += " AND Seats = @Seats";
                cmd.Parameters.AddWithValue("@Seats", seats);
            }
        }
    }
}

