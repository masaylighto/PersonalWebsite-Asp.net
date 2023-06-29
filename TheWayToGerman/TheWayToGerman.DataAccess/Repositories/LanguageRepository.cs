using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

public class LanguageRepository : ILanguageRepository
{ 

    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public ILog Log { get; }

    public LanguageRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
        Log = log;
    }

    public async Task<Result<bool>> IsExistAsync(Func<Language, bool> where)
    {        
      return await Task.Run<Result<bool>>(()=> 
      {
          try
          {
              return PostgresDBContext.Languages.Any(where);
          }
          catch (Exception ex)
          {
              Log.Error(ex);
              return new InternalErrorException("failed to check if language exist");
          }               
      });
    }

    public async Task<Result<OK>> AddAsync(Language language)
    {
        try
        {
            var IsExistResult = await IsExistAsync((lang) =>
                lang.LanguageName     == language.LanguageName &&
                lang.WritingDirection == language.WritingDirection
            );
            if (IsExistResult.ContainData() && IsExistResult.GetData())
            {
                return new UniqueFieldException("language already exist");
            }
            await PostgresDBContext.Languages.AddAsync(language);
            return new OK();
        }
        catch (Exception ex)
        {
           Log.Error(ex);
           return new InternalErrorException("failed to add new Langauge");
        }
       
    }

    public async Task<Result<Language>> GetAsync(Func<Language, bool> where)
    {
        return await Task.Run<Result<Language>>(() =>
        {
            try
            {
                return PostgresDBContext.Languages.FirstOrDefault(where);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new InternalErrorException("Langauge does not exist");
            }
        });
    }
}
