namespace Otw.Core.Domain;

public interface IWordsRepository
{
    public Task<WordEntity[]> GetAllAsync();
    public Task<WordEntity?> GetByIdAsync(int id);
}
