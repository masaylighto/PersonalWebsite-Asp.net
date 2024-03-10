#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Bogus;
using System.Net.Http.Json;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using PersonalWebsiteApi.Core.Enums;
using Core.HTTP.Interfaces;

namespace IntegrationTest.Endpoints;

public class CategoryTest
{
    readonly Faker Faker = new Faker();
    readonly IGenericHttpClient client;

    public CategoryTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }

    [Fact]
    public async Task AddCategory_CorrectInformation_ShouldReturnOK()
    {
       
    }


    [Fact]
    public async Task AddCategory_DuplicateName_ShouldReturnHttpBadRequest()
    {
    }

}
