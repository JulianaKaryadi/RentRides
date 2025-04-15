using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarRental.interpreter
{
    public class CarFilterContext
    {
        private List<IExpression> expressions = new List<IExpression>();

        public void AddExpression(IExpression expression)
        {
            expressions.Add(expression);
        }

        public void Interpret(SqlCommand cmd)
        {
            foreach (var expression in expressions)
            {
                expression.Interpret(cmd);
            }
        }
    }
}
