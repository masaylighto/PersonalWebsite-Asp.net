using Bogus;
using Core.DataKit.MockWrapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;
using TheWayToGerman.DataAccess.Repositories;
using TheWayToGerman.Core.Enums;
namespace UnitTest.DataAccess.Repositories;

public class LanguageRepositoryTest
{

    readonly Faker FakeData = new Faker();

    readonly Faker<Language> FakeLanguage = new Faker<Language>().CustomInstantiator((faker) =>
    {
        return new Language()
        {
            LanguageName = faker.Name.FullName(),
            WritingDirection = LanguageWritingDirection.RightToLeft
        };
    });
    readonly PostgresDBContext postgresDB;
    readonly ILanguageRepository LanguageRespository;
    public LanguageRepositoryTest()
    {
        postgresDB = FakeDBContext.Create();
        Mock<ILog> logMock = new Mock<ILog>();
        LanguageRespository = new LanguageRepository(postgresDB, new DateTimeProvider(), logMock.Object);
    }
    [Fact]
    public async Task AddAsync_CorrectInformation_ShouldBeAdded()
    {
        //Prepare
        Language language = FakeLanguage.Generate();
        //Execute
        var result = await LanguageRespository.AddAsync(language);
        var saveResult=  await postgresDB.SaveChangesAsync();
        //Validate
        Assert.True(result.ContainData());
        Assert.True(saveResult > 0);
    }
    [Fact]
    public async Task AddAsync_DublicatedName_ShouldBeReturnUniqueFieldException()
    {
        //Prepare
        Language language = FakeLanguage.Generate();
        await LanguageRespository.AddAsync(language);
        await postgresDB.SaveChangesAsync();
        //Execute
        var result = await LanguageRespository.AddAsync(language);
        //Validate
        Assert.True(result.IsErrorType<UniqueFieldException>());
    }
    [Fact]
    public async Task IsExistAsync_CategoryExist_ShouldBeReturnTrue()
    {
        //Prepare
        Language language = FakeLanguage.Generate();
        await LanguageRespository.AddAsync(language);
        await postgresDB.SaveChangesAsync();
        //Execute
        var result =await LanguageRespository.IsExistAsync((x) => 
            x.LanguageName == language.LanguageName
        );
        //Validate
        Assert.True(result.ContainData());
        Assert.True(result.GetData());
    }
    [Fact]
    public async Task IsExistAsync_CategoryExist_ShouldBeReturnDataNotFoundException()
    {
        //Prepare
        //Execute
        var result = await LanguageRespository.IsExistAsync((x) => 
            x.LanguageName == FakeData.Name.FullName()
        );

        //Validate
        Assert.True(result.ContainData());
        Assert.False(result.GetData());
    }
}
