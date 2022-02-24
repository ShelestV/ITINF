namespace Core;

public interface IAlternative
{
    int Index { get; set; }
    string Name { get; set; }
    MentionCollection Mentions { get; init; }
}
