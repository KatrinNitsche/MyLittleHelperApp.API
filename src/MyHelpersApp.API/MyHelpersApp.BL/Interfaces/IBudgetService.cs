using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.BL.Interfaces
{
    public interface IBudgetService
    {
        BudgetEntry Add(BudgetEntry budgetEntry);
        IEnumerable<BudgetEntry> GetAll();
        BudgetEntry Remove(int id);
        BudgetEntry Update(BudgetEntry budgetEntry, UpdatePattern updatePattern = UpdatePattern.none);
    }
}
