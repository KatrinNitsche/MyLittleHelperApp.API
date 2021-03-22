using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Repository
{
    public class BudgetRepository : IBudgetRepository
    {
        private DataContext context;

        public BudgetRepository(DataContext context)
        {
            this.context = context;
        }

        public BudgetEntry Add(BudgetEntry budgetEntry)
        {
            budgetEntry.Created = DateTime.Now;
            budgetEntry.Updated = null;
            context.BudgetEntries.Add(budgetEntry);
            context.SaveChanges();
            return budgetEntry;
        }

        public IEnumerable<BudgetEntry> GetAll()
        {
            return context.BudgetEntries;
        }

        public BudgetEntry Remove(int id)
        {
            var budgetEntry = context.BudgetEntries.Find(id);
            if (budgetEntry != null)
            {
                context.Remove(budgetEntry);
                context.SaveChanges();
            }

            return budgetEntry;
        }

        public BudgetEntry Update(BudgetEntry budgetEntry)
        {
            var entry = context.BudgetEntries.Find(budgetEntry.Id);
            if (entry != null)
            {
                entry.Amount = budgetEntry.Amount;
                entry.BudgetDate = budgetEntry.BudgetDate;
                entry.Created = budgetEntry.Created;
                entry.Description = budgetEntry.Description;
                entry.Updated = DateTime.Now;
                entry.Description = budgetEntry.Description;
                entry.RepetitionType = budgetEntry.RepetitionType;
                context.SaveChanges();
            }

            return entry;
        }
    }
}
