using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class SettingsRepositoryShould
    {
        [Fact]
        public void ReturnSettings()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("InMemoryData")
            .Options;

            using (var context = new DataContext(options))
            {
                ISettingsRepository sut = new SettingsRepository(context);

                Settings newSettings = new Settings()
                {
                    Currency = "£",
                    DarkColour = "#0B2545",
                    MiddleColour = "#8DA9C4",
                    LightColour = "#EEF4ED"
                };

                Settings savedSettings = sut.Save(newSettings);

                Settings settings = sut.Load();
                Assert.NotNull(settings);
                Assert.Equal("£", settings.Currency);
            }
        }

        [Fact]
        public void SaveSettings()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                ISettingsRepository sut = new SettingsRepository(context);

                Settings newSettings = new Settings()
                {
                    Currency = "£",
                    DarkColour = "#0B2545",
                    MiddleColour = "#8DA9C4",
                    LightColour = "#EEF4ED"
                };

                Settings savedSettings = sut.Save(newSettings);

                Assert.NotNull(savedSettings);
                Assert.Equal("£", savedSettings.Currency);
            }
        }

        [Fact]
        public void UpdateSettings()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("InMemoryData")
            .Options;

            using (var context = new DataContext(options))
            {
                ISettingsRepository sut = new SettingsRepository(context);

                Settings newSettings = new Settings()
                {
                    Currency = "£",
                    DarkColour = "#0B2545",
                    MiddleColour = "#8DA9C4",
                    LightColour = "#EEF4ED"
                };

                Settings savedSettings = sut.Save(newSettings);

                Assert.NotNull(savedSettings);
                Assert.Equal("£", savedSettings.Currency);

                savedSettings.Currency = "$";
                Settings changedSettigns = sut.Save(savedSettings);

                Assert.NotNull(changedSettigns);
                Assert.Equal("$", changedSettigns.Currency);
            }
        }
    }
}
