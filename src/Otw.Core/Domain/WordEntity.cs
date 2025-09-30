namespace Otw.Core.Domain;

public class WordEntity
{
    public required int Index { get; init; }
    public required string Value { get; init; }
    public required string Translation { get; set; }
}
