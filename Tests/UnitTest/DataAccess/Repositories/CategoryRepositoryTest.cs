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

public class CategoryRepositoryTest
{

    readonly static Faker FakeData = new Faker();
    readonly static Language language  = new Language()
    {
        Id = Guid.NewGuid(),
        LanguageName = FakeData.Name.FirstName(),
        WritingDirection = LanguageWritingDirection.RightToLeft
    };
    readonly Faker<Category> FakeCategory = new Faker<Category>().CustomInstantiator((faker) =>
    {
        return new Category()
        {   Id = Guid.NewGuid(),
            Name = faker.Commerce.Categories(1).First(),        
            Language = language,
        };
    });
    readonly PostgresDBContext postgresDB;
    readonly ICategoryRepository CategoryRespository;
    public CategoryRepositoryTest()
    {
        postgresDB = FakeDBContext.Create();
        Mock<ILog> logMock = new Mock<ILog>();
        postgresDB.Languages.Add(language);
        CategoryRespository = new CategoryRepository(postgresDB, new DateTimeProvider(), logMock.Object);
    }
    [Fact]
    public async Task AddAsync_CorrectInformation_ShouldBeAdded()
    {
        //Prepare
        Category category = FakeCategory.Generate();
        //Execute
        var result = await CategoryRespository.AddAsync(category);
        var saveResult=await postgresDB.SaveChangesAsync();
        //Validate
        Assert.True(result.ContainData());
        Assert.True(saveResult>0);
    }
    [Fact]
    public async Task AddAsync_DublicatedName_ShouldBeReturnUniqueFieldException()
    {
        //Prepare
        Category category = FakeCategory.Generate();
        Category category2 = FakeCategory.Generate();
        category2.Name = category.Name; 
        await CategoryRespository.AddAsync(category);
        await postgresDB.SaveChangesAsync();
        //Execute
        var result = await CategoryRespository.AddAsync(category2);
        //Validate
        Assert.True(result.IsErrorType<UniqueFieldException>());
    }
    [Fact]
    public async Task IsExistAsync_CategoryExist_ShouldBeReturnTrue()
    {
        //Prepare
        Category category = FakeCategory.Generate();
        await CategoryRespository.AddAsync(category);
        await postgresDB.SaveChangesAsync();
        //Execute
        var result =await CategoryRespository.IsExistAsync((x) => x.Name == category.Name);
        //Validate
        Assert.True(result.ContainData());
        Assert.True(result.GetData());
    }
    [Fact]
    public async Task IsExistAsync_CategoryExist_ShouldBeReturnDataNotFoundException()
    {
        //Prepare
        //Execute
        var result = await CategoryRespository.IsExistAsync((x) => 
            x.Name == FakeData.Name.FullName()
        );

        //Validate
        Assert.True(result.ContainData());
        Assert.False(result.GetData());
    }
}
