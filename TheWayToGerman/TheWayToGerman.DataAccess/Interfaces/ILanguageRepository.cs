using Core.DataKit;
using Core.DataKit.Result;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface ILanguageRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Language, bool> where);
    public Task<Result<OK>> AddAsync(Language language);
    public Task<Result<Language>> GetAsync(Func<Language, bool> where);
}
