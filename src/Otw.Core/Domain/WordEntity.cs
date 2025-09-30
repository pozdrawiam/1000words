namespace Otw.Core.Domain;

public sealed class WordEntity
{
    public required int Id { get; init; }
    public required string Value { get; init; }
    public required string Translation { get; set; }
}
