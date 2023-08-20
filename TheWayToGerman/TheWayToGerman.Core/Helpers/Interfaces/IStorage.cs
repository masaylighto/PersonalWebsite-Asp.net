using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;

namespace TheWayToGerman.Core.Helpers.Interfaces;

public interface IStorage
{
    Task<State> SaveAsync(byte[] fileContent, string filePath);
    Task<Result<byte[]>> LoadAsync(string filePath);
    Task<State> DeleteAsync(string filePath);
}
