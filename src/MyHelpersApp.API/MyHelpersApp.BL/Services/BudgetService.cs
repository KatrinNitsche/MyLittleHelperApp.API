using MyHelpersApp.BL.Interfaces;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.BL.Services
{
    public class BudgetService : IBudgetService
    {
        // which gives you 5 years of repetition for a weekly entry
        private const int _repeatedDatesNumber = 52 * 5;

        private IBudgetRepository budgetRepository;

        public BudgetService(IBudgetRepository toDoRepository)
        {
            this.budgetRepository = toDoRepository;
        }

        public BudgetEntry Add(BudgetEntry budgetEntry)
        {
            try
            {
                if (budgetEntry.RepetitionType != 0)
                {
                    var entry = this.budgetRepository.Add(budgetEntry);
                    var idOfFirstEntry = entry.Id;
                    for (int i = 0; i < _repeatedDatesNumber; i++)
                    {
                        var followUpEntry = new BudgetEntry()
                        {
                            Amount = budgetEntry.Amount,
                            Description = budgetEntry.Description,
                            RepetitionType = budgetEntry.RepetitionType,
                            RepitionOfId = idOfFirstEntry,
                            BudgetDate = HelperService.GetNextDate(entry.BudgetDate, entry.RepetitionType, i)
                        };

                        if (followUpEntry.BudgetDate.Date != entry.BudgetDate.Date) this.budgetRepository.Add(followUpEntry);
                    }

                    return entry;
                }
                else
                {
                    return this.budgetRepository.Add(budgetEntry);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BudgetEntry> GetAll()
        {
            try
            {
                return this.budgetRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BudgetEntry Remove(int id)
        {
            try
            {
                var existingEntry = this.GetAll().FirstOrDefault(x => x.Id == id);
                if (existingEntry != null)
                {
                    if (existingEntry.RepetitionType != 0)
                    {
                        var originalId = existingEntry.RepitionOfId != null ? existingEntry.RepitionOfId : existingEntry.Id;
                        var list = this.budgetRepository.GetAll().Where(b => b.RepitionOfId == originalId || b.Id == originalId).ToList();
                        foreach (var entry in list)
                        {
                            this.budgetRepository.Remove(entry.Id);
                        }
                    }
                    else
                    {
                        return this.budgetRepository.Remove(id);
                    }
                }

                return existingEntry;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BudgetEntry Update(BudgetEntry budgetEntry, UpdatePattern updatePattern = UpdatePattern.none)
        {
            try
            {
                var existingEntry = this.GetAll().FirstOrDefault(x => x.Id == budgetEntry.Id);
                if (existingEntry != null)
                {
                    if (existingEntry.RepetitionType != 0 && budgetEntry.RepetitionType != 0 && updatePattern != UpdatePattern.none)
                    {
                        Update_NoChangeInRepetition(budgetEntry, updatePattern);
                        return this.GetAll().FirstOrDefault(x => x.Id == budgetEntry.Id);
                    }
                    else
                    {
                        return this.budgetRepository.Update(budgetEntry);
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Update_NoChangeInRepetition(BudgetEntry budgetEntry, UpdatePattern updatePattern)
        {
            var existingEntry = this.GetAll().FirstOrDefault(x => x.Id == budgetEntry.Id);
            var originalId = existingEntry.RepitionOfId != null ? existingEntry.RepitionOfId : existingEntry.Id;
            var entriesToUpdate = this.budgetRepository.GetAll().Where(b => b.RepitionOfId == originalId);
            if (updatePattern == UpdatePattern.future)
            {
                entriesToUpdate = entriesToUpdate.Where(b => b.BudgetDate > existingEntry.BudgetDate);
            }

            BudgetEntry originalEntry = this.budgetRepository.GetAll().FirstOrDefault(b => b.Id == originalId);
            var firstDate = updatePattern == UpdatePattern.all ? originalEntry.BudgetDate : entriesToUpdate.ToList()[0].BudgetDate;
            var index = 0;
            foreach (var entry in entriesToUpdate)
            {
                entry.Amount = budgetEntry.Amount;
                entry.Description = budgetEntry.Description;
                entry.RepetitionType = budgetEntry.RepetitionType;
                this.budgetRepository.Update(entry);
                if (existingEntry.RepetitionType != budgetEntry.RepetitionType)
                {
                    entry.BudgetDate = HelperService.GetNextDate(firstDate, budgetEntry.RepetitionType, index);
                }

                index++;
            } 
            
            if (updatePattern == UpdatePattern.currentAndFutur || updatePattern == UpdatePattern.all)
            {
                this.budgetRepository.Update(budgetEntry);
            }
        }       
    }
}