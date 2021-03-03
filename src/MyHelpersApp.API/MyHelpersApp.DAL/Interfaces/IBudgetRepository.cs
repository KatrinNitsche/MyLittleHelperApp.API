using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface IBudgetRepository
    {
        BudgetEntry Add(BudgetEntry budgetEntry);
        IEnumerable<BudgetEntry> GetAll();
        BudgetEntry Remove(int id);
        BudgetEntry Update(BudgetEntry budgetEntry);
    }
}
