using MonopolyStorage.DataAccess.IO.Options;
using MonopolyStorage.Domain.Services;
using System.Text.Json;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace MonopolyStorage.DataAccess.IO
{
    public class DataIOService(DataIOOptions options) : IDataIOService
    {
        private readonly DataIOOptions _options = options;
        private static Dictionary<Type, PropertyInfo[]> _entities = [];

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
            [typeof(DateOnly)] = (string input, out object? value) => 
            {
                if (DateOnly.TryParse(input, out var parsed))
                {
                    value = parsed;
                    return true;
                }
                value = default; return false;
            },
            [typeof(DateTime)] = (string input, out object? value) =>
            {
                if (DateTime.TryParse(input, out var parsed))
                {
                    value = parsed;
                    return true;
                }
                value = default; return false;
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
            [typeof(double)] = (string input, out object? value) =>
            {
                if (double.TryParse(input, out var parsed))
                {
                    value = parsed;
                    return true;
                }
                value = default; return false;
            },
            [typeof(Guid)] = (string input, out object? value) =>
            {
                if (Guid.TryParse(input, out var parsed))
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

        private static T GetEntityFromStringJSON<T>(string line)
        {
            return JsonSerializer.Deserialize<T>(line) ?? throw new ArgumentException("");
        }

        private static T GetEntityFromStringCSV<T>(string[] headers, string[] values) where T : new()
        {
            if (!_entities.TryGetValue(typeof(T), out PropertyInfo[]? properties) || properties == null)
            {
                properties = typeof(T).GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy);

                _entities.Add(typeof(T), properties);
            }

            var obj = new T();
            for (int i = 0; i < values.Length; i++) 
            {
                var prop = properties.Where(p => p.Name.Equals(headers[i], StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault() 
                    ?? throw new ArgumentException($"Заданные заголовки ({headers[i]}) не совпадают с заголовками сущности {typeof(T)}");
                
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (_stringConverters.TryGetValue(type, out var tryConvert))
                {
                    if (tryConvert(values[i], out var converted))
                    {
                        prop.SetValue(obj, converted);
                    }
                    else
                    {
                        throw new ArgumentException($"Не удалось преобразовать тип {nameof(prop.PropertyType)}: {nameof(prop.Name)}");
                    }
                }                
            }
            return obj;
        }

        private IEnumerable<T> ReadCSV<T>(string filePath) where T : new()
        {
            using var reader = new StreamReader(filePath);
            string? line = reader.ReadLine();

            string[] headers = _options.CSVHeaders
                ?? line?.Split(_options.CSVDataDelimiter)
                ?? throw new ArgumentException("Отсутствуют заголовки CSV файла с данными!");

            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                yield return GetEntityFromStringCSV<T>(headers, line.Split(_options.CSVDataDelimiter));       
            }
        }

        private static IEnumerable<T> ReadJSON<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            string? line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                yield return GetEntityFromStringJSON<T>(line);
            }
        }

        public IEnumerable<T> ReadEntitiesFromFile<T>(string filePath) where T : new()
        {
            return _options.FormatType switch
            {
                FormatType.CSV => ReadCSV<T>(filePath),
                FormatType.JSON => ReadJSON<T>(filePath),
                _ => throw new ArgumentException($"Неизвестный формат файла. Проверьте настройки конфигурации {nameof(DataIOOptions)}"),
            };
        }
    }
}
