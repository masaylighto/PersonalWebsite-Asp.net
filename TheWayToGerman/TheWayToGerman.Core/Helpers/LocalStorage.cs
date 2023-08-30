
using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using TheWayToGerman.Core.Configuration;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers.Interfaces;

namespace TheWayToGerman.Core.Helpers;

public class LocalStorage : IStorage
{
    public LocalStorage(LocalStorageConfiguration localStorageConfiguration)
    {
        LocalStorageConfiguration = localStorageConfiguration;
        Directory.CreateDirectory(LocalStorageConfiguration.PathPrefix);
    }

    public LocalStorageConfiguration LocalStorageConfiguration { get; }

    public async Task<State> DeleteAsync(string filePath)
    {     
        string fullPath = $"{LocalStorageConfiguration.PathPrefix}/{filePath}";
        if (!File.Exists(fullPath))
        {
            return new FileNotFoundException($"{fullPath} not exist");
        }
        try
        {
             File.Delete(fullPath);
             return new OK();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Result<byte[]>> LoadAsync(string filePath)
    {
        string fullPath = $"{LocalStorageConfiguration.PathPrefix}/{filePath}";
        if (!File.Exists(fullPath))
        {
            return new FileNotFoundException($"{filePath} not exist");
        }
        try
        {
            return await File.ReadAllBytesAsync(fullPath);
        }
        catch (Exception ex)
        {
            return ex;
        }

    }

    public async Task<State> SaveAsync(byte[] fileContent, string filePath)
    {
        
        string fullPath = $"{LocalStorageConfiguration.PathPrefix}/{filePath}";
        if (File.Exists(fullPath))
        {
            return new FileAlreadyExistException($"{fullPath} already exist");
        }
        try
        {
            File.WriteAllBytes(fullPath, fileContent);
            return new OK();
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
}
