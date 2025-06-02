using System.Reflection;
using System.Text.RegularExpressions;

namespace MonopolyStorage.Presentation.Interactive.Commands.Base
{
    public class CommandExecuter(Dictionary<string, Command> commands)
    {
        private Dictionary<string, Command> _commands { get; set; } = commands;

        private delegate bool TryConvertString(string rawValue, out object? value);
        private readonly static Dictionary<Type, TryConvertString> _stringConverters = new()
        {
            [typeof(bool)] = (string rawValue, out object? value) =>
            {
                if (bool.TryParse(rawValue, out var parsed))
                {
                    value = parsed;
                    return true;
                }
                value = default;
                return false;
            },
            [typeof(int)] = (string input, out object? value) =>
            {
                if (int.TryParse(input, out var parsed))
                {
                    value = parsed;
                    return true;
                }
                value = default; return false;
            },
            [typeof(string)] = (string input, out object? value) =>
            {
                value = input;
                return true;
            }
        };

        private static Dictionary<string, PropertyInfo[]> _commandsCache = [];

        private readonly Regex OptCleanRegex = new("[^a-zA-Z]");

        public void Invoke(string? line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("Пустая строка вместо команды.");
            var parts = line.Split(' ');

            if (parts[0] == "-h")
            {
                var helpString = _commands
                    .Select(c => $"{c.Key} - {c.Value.Description};\n")
                    .Aggregate((acc, s) => acc += s);
                Console.WriteLine(helpString);
                return;
            }

            if (!_commands.TryGetValue(parts[0], out var command))
                throw new ArgumentException($"Неизвестная команда {parts[0]}");

            if (parts.Length > 1 && parts[1] == "-h")
            {
                var helpString = (command.Options.Count > 0)
                    ? command.Options.Select(o => $"{o.Name} - {o.Description}\n").Aggregate((acc, s) => acc += s)
                    : string.Empty;
                Console.WriteLine($"{command.Name} - {command.Description}\nОпции:\n{helpString}");
                return;
            }

            foreach (var opt in command.Options)
            {
                var index = Array.FindIndex(parts, p => p.StartsWith(opt.Name));
                if (index == -1 && opt.IsRequired)
                    throw new ArgumentException($"Опция {opt.Name} для команды {command.Name} обязательна");

                if (index == -1)
                    continue;

                if (index + 1 >= parts.Length)
                    throw new ArgumentException($"Для опции {opt.Name} отсутствует аргумент");
                var optArg = parts[index + 1];

                var optType = opt.GetType().GetGenericArguments()[0];
                if (_stringConverters.TryGetValue(optType, out var tryConvert))
                {
                    if (tryConvert(optArg, out var converted))
                    {
                        if (!_commandsCache.TryGetValue(command.Name, out var props) || props == null)
                        {
                            props = command.GetType().GetProperties();
                            _commandsCache.Add(command.Name, props);
                        }
                        var formattedOptName = OptCleanRegex.Replace(opt.Name, string.Empty);
                        var optProp = props.FirstOrDefault(p => p.Name.Equals(formattedOptName, StringComparison.CurrentCultureIgnoreCase));
                        if (optProp == null) continue;
                        optProp.SetValue(command, converted);
                    }
                }
            }
            command.Execute();
            Console.WriteLine($"Команда {command.Name} выполнена");
        }
    }
}
