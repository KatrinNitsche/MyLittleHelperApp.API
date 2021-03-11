using MyHelpersApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface ISettingsRepository
    {
        Settings Save(Settings newSettings);
        Settings Load();
    }
}
