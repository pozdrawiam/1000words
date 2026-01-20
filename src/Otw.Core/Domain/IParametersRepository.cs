using Otw.Core.Domain;

namespace Otw.Core.Domain;

public interface IParametersRepository
{
    Task<int?> GetLearnLastWordIdAsync();
    Task SetLearnLastWordIdAsync(int id);
    Task<int?> GetReviewLastWordIdAsync();
    Task SetReviewLastWordIdAsync(int id);
    Task<WordSortType> GetLearnSortTypeAsync();
    Task SetLearnSortTypeAsync(WordSortType sortType);
    Task<WordSortType> GetReviewSortTypeAsync();
    Task SetReviewSortTypeAsync(WordSortType sortType);
    Task ResetLearnProgressAsync();
    Task ResetReviewProgressAsync();
    Task<bool> IsLearnStartedAsync();
    Task SetLearnStartedAsync();
}
