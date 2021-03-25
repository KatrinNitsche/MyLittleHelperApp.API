using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
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
        private readonly IBudgetRepository budgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            this.budgetRepository = budgetRepository;
        }

        [HttpGet]
        public IEnumerable<BudgetEntry> Get(bool thisMonth = false)
        {
            if (thisMonth)
            {
                return this.budgetRepository.GetAll().Where(be => be.BudgetDate.Date.Year == DateTime.Now.Year && be.BudgetDate.Date.Month == DateTime.Now.Month);
            }
            else
            {
                return this.budgetRepository.GetAll().OrderBy(b => b.BudgetDate);
            }
        }

        [HttpPost]
        public BudgetEntry Post(BudgetEntry budgetEntry)
        {
            if (budgetEntry.Id == 0)
            {
                this.budgetRepository.Add(budgetEntry);
            }
            else
            {
                this.budgetRepository.Update(budgetEntry);
            }

            return budgetEntry;
        }

        [HttpDelete]
        public BudgetEntry Delete(int id)
        {
            return this.budgetRepository.Remove(id);
        }
    }
}