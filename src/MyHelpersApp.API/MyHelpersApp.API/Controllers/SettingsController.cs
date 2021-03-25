using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository settingsRepository;

        public SettingsController(ISettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        [HttpGet]
        public Settings Get()
        {
            return this.settingsRepository.Load();
        }

        [HttpPost]
        public Settings Post(Settings settings)
        {
            return this.settingsRepository.Save(settings);
        }
    }
}