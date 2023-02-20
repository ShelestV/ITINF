using System.Text;
using System.Text.Json;
using PaymentSystem.Domain;

namespace PaymentSystem.Persistence;

public abstract class JsonFileRepository<TModel> where TModel : Model
{
    private const string FilePath = "C://PaymentSystem//employee.json";

    protected static void Save(string data)
    {
        using var writer = new StreamWriter(FilePath);
        writer.Write(data);
    }

    protected static IEnumerable<JsonElement> GetData()
    {
		Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        if (File.Exists(FilePath))
        {
            using var reader = new StreamReader(FilePath);
            var data = reader.ReadToEnd();
            var models = JsonSerializer.Deserialize<JsonElement[]>(data)!;
            return models;
        }
        else
        {
            using var stream = File.Create(FilePath);
            var buffer = Encoding.Default.GetBytes("[]");
            stream.Write(buffer);
            return new List<JsonElement>();
        }
    }
}