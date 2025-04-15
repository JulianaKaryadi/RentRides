using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.interpreter
{
    public class RateExpression : IExpression
    {
        private decimal? minRate;
        private decimal? maxRate;

        public RateExpression(decimal? minRate, decimal? maxRate)
        {
            this.minRate = minRate;
            this.maxRate = maxRate;
        }

        public void Interpret(SqlCommand cmd)
        {
            if (minRate.HasValue)
            {
                cmd.CommandText += " AND DailyRate >= @MinRate";
                cmd.Parameters.AddWithValue("@MinRate", minRate);
            }
            if (maxRate.HasValue)
            {
                cmd.CommandText += " AND DailyRate <= @MaxRate";
                cmd.Parameters.AddWithValue("@MaxRate", maxRate);
            }
        }
    }
}

