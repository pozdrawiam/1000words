namespace Otw.Core.Domain;

public interface IWordsRepository
{
    public Task<WordEntity[]> GetAll();
}
