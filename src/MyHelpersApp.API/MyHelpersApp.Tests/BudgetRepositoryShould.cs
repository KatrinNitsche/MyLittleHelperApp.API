using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class BudgetRepositoryShould
    {
        [Fact]
        public void ReturnBudgetEntries()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository sut = new BudgetRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new BudgetEntry()
                    {
                        Description = $"Budget Entry {i + 1}",
                        Amount = 10 * i
                    });
                }

                var budgetList = sut.GetAll().ToList();
                Assert.NotNull(budgetList);
                Assert.True(budgetList.Any());
            }
        }

        [Fact]
        public void RemoveBudgetEntry()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository sut = new BudgetRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new BudgetEntry()
                    {
                        Description = $"Budget Entry {i + 1}",
                        Amount = 10 * i
                    });
                }

                var budgetList = sut.GetAll().ToList();
                foreach (var budgetEntry in budgetList)
                {
                    sut.Remove(budgetEntry.Id);
                }

                budgetList = sut.GetAll().ToList();
                Assert.NotNull(budgetList);
                Assert.False(budgetList.Any());
            }
        }

        [Fact]
        public void UpdateBudgetEntry()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase("InMemoryData")
             .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository sut = new BudgetRepository(context);

                sut.Add(new BudgetEntry()
                {
                    Description = $"Budget Entry",
                    Amount = 10
                });


                var budgetList = sut.GetAll().ToList();
                var budgetEntry = new BudgetEntry()
                {
                    Amount = 20,
                    BudgetDate = budgetList[0].BudgetDate,
                    Created = budgetList[0].Created,
                    Description = "Updated Description Text",
                    Id = budgetList[0].Id,
                    Updated = budgetList[0].Updated
                };

                var updatedEntry = sut.Update(budgetEntry);
                budgetList = sut.GetAll().ToList();

                Assert.NotNull(updatedEntry);
                Assert.Equal(20, budgetList[0].Amount);
                Assert.Equal("Updated Description Text", budgetList[0].Description);
            }
        }
    }
}
