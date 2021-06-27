using api.NET.Controllers;
using api.NET.Models;
using api.NET.Views;
using Microsoft.EntityFrameworkCore;
using System;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace api.NET.Test
{
    public class CharacterControllerTest
    {
        DisneyDbContext databaseContext;
        public CharacterControllerTest()
        {
            var options = new DbContextOptionsBuilder<DisneyDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            databaseContext = new DisneyDbContext(options);
            databaseContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetCharacters_Returns_The_Correct_Number_Characters()
        {
            var count = 3;
            var fakeCharacters = A.CollectionOfDummy<Character>(count);

            databaseContext.Character.AddRange(fakeCharacters);
            await databaseContext.SaveChangesAsync();

            var controller = new CharacterController(databaseContext);

            var actionResult = await controller.GetCharacters(null, null, null);
            var result = actionResult as List<CharacterDTO>;
            Assert.Equal(count, result.Count);

        }
    }
}
