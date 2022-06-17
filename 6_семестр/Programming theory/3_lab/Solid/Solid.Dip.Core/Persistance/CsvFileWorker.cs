namespace Solid.Dip.Persistance;

public class CsvFileWorker : IFileWorkable
{
    private const string FileName = "employees.csv";

    private readonly Loggers.ILogger logger = Loggers.ConsoleLogger.Instance;

    public IList<string> ReadLines()
    {
        var lines = new List<string>();

        try
        {
            using var stream = new StreamReader(FileName);
            var line = stream.ReadLine(); // csv description
            
            while ((line = stream.ReadLine()) is not null)
            {
                lines.Add(line);
            }
        }
        catch (FileNotFoundException ex)
        {
            this.logger.Error($"Not exist file with name {FileName}", ex);
        }

        return lines;
    }

    public void WriteLine(string line)
    {
        try
        {
            using var stream = new StreamWriter(FileName, append: true);
            stream.WriteLine(line);
        }
        catch (FileNotFoundException ex)
        {
            this.logger.Error($"Not exist file with name {FileName}", ex);
        }
    }
}
