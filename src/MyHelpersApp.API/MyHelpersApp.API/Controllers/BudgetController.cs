using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.BL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService budgetService;

        public BudgetController(IBudgetService budgetRepository)
        {
            this.budgetService = budgetRepository;
        }

        [HttpGet]
        public IEnumerable<BudgetEntry> Get(bool thisMonth = false)
        {
            if (thisMonth)
            {
                return this.budgetService.GetAll().Where(be => be.BudgetDate.Date.Year == DateTime.Now.Year && be.BudgetDate.Date.Month == DateTime.Now.Month);
            }
            else
            {
                return this.budgetService.GetAll().OrderBy(b => b.BudgetDate);
            }
        }

        [HttpPost]
        public BudgetEntry Post(BudgetEntry budgetEntry)
        {
            if (budgetEntry.Id == 0)
            {
                this.budgetService.Add(budgetEntry);
            }
            else
            {
                this.budgetService.Update(budgetEntry);
            }

            return budgetEntry;
        }

        [HttpDelete]
        public BudgetEntry Delete(int id)
        {
            return this.budgetService.Remove(id);
        }
    }
}