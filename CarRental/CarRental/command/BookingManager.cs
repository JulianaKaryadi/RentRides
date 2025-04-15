using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.command
{
    public class BookingManager
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }

            _commands.Clear();
        }
    }
}
