namespace Solid.Dip.Documents;

internal class WorkContract : IJsonExportable, ITxtExportable, IPdfExportable
{
    private readonly string content;

    public WorkContract(string content)
    {
        this.content = content;
    }

    public byte[] ToPdf()
    {
        return System.Text.Encoding.ASCII.GetBytes(this.content);
    }

    public string ToJson()
    {
        return $"'content': '{this.content}'";
    }

    public string ToTxt()
    {
        return this.content;
    }
}
