using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Linq;

namespace MyHelpersApp.DAL.Repository
{
    public class SettingsRepository : ISettingsRepository
    {
        private DataContext context;

        public SettingsRepository(DataContext context)
        {
            this.context = context;
        }

        public Settings Load()
        {
            var allSettings = context.Settings.ToList();
            var settings = context.Settings.FirstOrDefault();
            if (settings == null)
            {
                Settings newSettings = new Settings()
                {
                    Currency = "£",
                    DarkColour = "#0B2545",
                    MiddleColour = "#8DA9C4",
                    LightColour = "#EEF4ED"
                };

                context.Settings.Add(newSettings);
                return newSettings;
            }

            return settings;
        }

        public Settings Save(Settings newSettings)
        {
            var settings = context.Settings.FirstOrDefault();
            if (settings == null)
            {                
                context.Add(newSettings);
                settings = newSettings;
            }
            else
            {
                settings.Currency = newSettings.Currency;
                settings.DarkColour = newSettings.DarkColour;
                settings.LightColour = newSettings.LightColour;
                settings.MiddleColour = newSettings.MiddleColour;
            }

            context.SaveChanges();
            return settings;
        }
    }
}
