
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.DataAccess.Interfaces;

public interface IOwnerRepository
{
    public Task<Result<User>> GetAsync();
    public Task<Result<OK>> IsOwnerExistAsync();
    public Task<Result<Guid>> AddAsync(User user);
    public Task<Result<OK>> UpdateAsync(User user);
}
