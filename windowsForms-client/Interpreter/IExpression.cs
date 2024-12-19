using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace windowsForms_client.Interpreter
{
    public interface IExpression
    {
        void Interpret(InterpreterContext context, SynchronizationContext syncContext, GameClientFacade form);
    }

    public class InterpreterContext
    {
        public string Parameter1 { get; set; } 
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; } 
    }

    public class ChangeColor : IExpression
    {
        public void Interpret(InterpreterContext context, SynchronizationContext syncContext, GameClientFacade form)
        {
            var color = context.Parameter1;
            if (color == "purple")
            {
                form.ChangeTankColor(Color.Purple);
            }
            else if (color == "green")
            {
                form.ChangeTankColor(Color.Green);
            }
            else if (color == "yellow")
            {
                form.ChangeTankColor(Color.Yellow);
            }
            else
            {
                Console.WriteLine("unknown color - try: purple, green, yellow");
            }

        }
    }
    public class Help : IExpression
    {
        public void Interpret(InterpreterContext context, SynchronizationContext syncContext, GameClientFacade form)
        {
            Console.WriteLine("Use '/help' to see available commands");
            Console.WriteLine("Use '/changecolor <color>' to change your tank's color");
        }
    }
    public class Health : IExpression
    {
        public void Interpret(InterpreterContext context, SynchronizationContext syncContext, GameClientFacade form)
        {
            Console.WriteLine($"Current health = {form.GetHealth()}");
        }
    }


    public class CommandParser
    {
        private readonly Dictionary<string, IExpression> _commands = new();

        public CommandParser()
        {
            _commands["/changecolor"] = new ChangeColor();
            _commands["/help"] = new Help();
            _commands["/health"] = new Health();
        }

        public void ParseAndExecute(string input, SynchronizationContext syncContext, GameClientFacade form)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;

            var commandName = parts[0].ToLower();
            var context = new InterpreterContext
            {
                Parameter1 = parts.Length > 1 ? parts[1] : null,
                Parameter2 = parts.Length > 2 ? parts[2] : null,
                Parameter3 = parts.Length > 3 ? parts[3] : null
            };
            if (_commands.TryGetValue(commandName, out var expression))
            {
                expression.Interpret(context, syncContext, form);
            }
            else
            {
                Console.WriteLine($"Unknown command: {commandName} - try /help");
            }
        }
    }

}
