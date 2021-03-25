using Microsoft.EntityFrameworkCore;
using MyHelpersApp.BL.Interfaces;
using MyHelpersApp.BL.Services;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class BudgetServiceShould
    {
        [Fact]
        public void ReturnBudgetEntries()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new BudgetEntry()
                    {
                        Amount = 100,
                        BudgetDate = new DateTime(2021, 3, 23),
                        Description = $"Test Entry {i + 1}",
                        RepetitionType = (int)RepetitionType.none
                    });
                }

                var list = sut.GetAll().ToList();
                Assert.NotNull(list);
                Assert.True(list.Any());
            }
        }

        [Fact]
        public void RemoveBudgetEntries()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase("InMemoryData")
               .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new BudgetEntry()
                    {
                        Amount = 100,
                        BudgetDate = new DateTime(2021, 3, 23),
                        Description = $"Test Entry {i + 1}",
                        RepetitionType = (int)RepetitionType.none
                    });
                }

                var list = sut.GetAll().ToList();

                foreach (var entry in list)
                {
                    sut.Remove(entry.Id);
                }

                list = sut.GetAll().ToList();
                Assert.NotNull(list);
                Assert.False(list.Any());
            }
        }

        [Fact]
        public void UpdateBudgetEntries()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                sut.Add(new BudgetEntry()
                {
                    Amount = 100,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Test Entry",
                    RepetitionType = (int)RepetitionType.none
                });

                var list = sut.GetAll().ToList();
                var entryToChange = new BudgetEntry()
                {
                    Amount = 200,
                    BudgetDate = list[0].BudgetDate,
                    Created = list[0].Created,
                    Description = "Updated Test Entry",
                    Id = list[0].Id,
                    RepetitionType = list[0].RepetitionType,
                    RepitionOfId = list[0].RepitionOfId,
                    Updated = list[0].Updated
                };

                sut.Update(entryToChange);
                var updatedList = sut.GetAll().ToList();

                Assert.NotNull(updatedList);
                var changedEntry = updatedList.Last();

                Assert.Equal("Updated Test Entry", changedEntry.Description);
                Assert.Equal(200, changedEntry.Amount);
            }
        }

        [Fact]
        public void CreateFollowUpEntriesForRepetitionWeekly()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase("InMemoryData")
             .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().Where(x => x.RepitionOfId == checkEntry.Id).ToList();

                Assert.Equal(52 * 5, list.Count());
                Assert.Equal("Weekly Test Entry", list[0].Description);
                Assert.Equal(10, list[0].Amount);
                Assert.Equal(new DateTime(2021, 3, 30), list[0].BudgetDate);
            }
        }

        [Fact]
        public void CreateFollowUpEntriesForRepetitionMonthly()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase("InMemoryData")
             .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Monthly Test Entry",
                    RepetitionType = (int)RepetitionType.monthly
                });

                var list = sut.GetAll().Where(x => x.RepitionOfId == checkEntry.Id).ToList();

                Assert.Equal(52 * 5, list.Count());
                Assert.Equal("Monthly Test Entry", list[0].Description);
                Assert.Equal(10, list[0].Amount);
                Assert.Equal(new DateTime(2021, 4, 23), list[0].BudgetDate);
            }
        }

        [Fact]
        public void CreateFollowUpEntriesForRepetitionYearly()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase("InMemoryData")
             .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Yearly Test Entry",
                    RepetitionType = (int)RepetitionType.yearly
                });

                var list = sut.GetAll().Where(x => x.RepitionOfId == checkEntry.Id).ToList();

                Assert.Equal(52 * 5, list.Count());
                Assert.Equal("Yearly Test Entry", list[0].Description);
                Assert.Equal(10, list[0].Amount);
                Assert.Equal(new DateTime(2022, 3, 23), list[0].BudgetDate);
            }
        }

        [Fact]
        public void UpdateFolloUpEntriesForRepitionMonthly_Current()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("InMemoryData")
            .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry To Update",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().ToList();
                var entryToChange = new BudgetEntry()
                {
                    Amount = 15,
                    BudgetDate = list[0].BudgetDate,
                    Created = list[0].Created,
                    Description = "Updated Test Entry",
                    Id = list[0].Id,
                    RepetitionType = list[0].RepetitionType,
                    RepitionOfId = list[0].RepitionOfId,
                    Updated = list[0].Updated
                };

                var check = sut.Update(entryToChange);

                Assert.NotNull(check);
                Assert.Equal("Updated Test Entry", check.Description);
                Assert.Equal(15, check.Amount);

                var followUp = sut.GetAll().Where(x => x.RepitionOfId == check.Id);
                Assert.Equal(10, followUp.First().Amount);
                Assert.Equal("Weekly Test Entry To Update", followUp.First().Description);
            }
        }

        [Fact]
        public void UpdateFolloUpEntriesForRepitionWeekly_All()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("InMemoryData")
            .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry To Update",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().ToList();
                var entryToChange = new BudgetEntry()
                {
                    Amount = 15,
                    BudgetDate = list[0].BudgetDate,
                    Created = list[0].Created,
                    Description = "Updated Test Entry",
                    Id = list[0].Id,
                    RepetitionType = list[0].RepetitionType,
                    RepitionOfId = list[0].RepitionOfId,
                    Updated = list[0].Updated
                };

                var check = sut.Update(entryToChange, UpdatePattern.all);

                Assert.NotNull(check);
                Assert.Equal("Updated Test Entry", check.Description);
                Assert.Equal(15, check.Amount);

                var followUp = sut.GetAll().Where(x => x.RepitionOfId == check.Id);
                Assert.DoesNotContain(followUp, x => x.Amount == 10);
                Assert.DoesNotContain(followUp, x => x.Description == "Weekly Test Entry To Update");
            }
        }

        [Fact]
        public void UpdateFollowEntriesForRepetitionChange_All()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry To Update",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().ToList();
                var entryToChange = new BudgetEntry()
                {
                    Amount = 15,
                    BudgetDate = list[0].BudgetDate,
                    Created = list[0].Created,
                    Description = "Updated Test Entry To Monthly",
                    Id = list[0].Id,
                    RepetitionType = (int)RepetitionType.monthly,
                    RepitionOfId = list[0].RepitionOfId,
                    Updated = list[0].Updated
                };

                var check = sut.Update(entryToChange, UpdatePattern.all);

                Assert.NotNull(check);
                Assert.Equal("Updated Test Entry To Monthly", check.Description);
                Assert.Equal(15, check.Amount);
                Assert.Equal(new DateTime(2021, 3, 23), check.BudgetDate);

                var followUp = sut.GetAll().Where(x => x.RepitionOfId == check.Id).ToList();
                Assert.DoesNotContain(followUp, x => x.Amount == 10);
                Assert.DoesNotContain(followUp, x => x.Description == "Weekly Test Entry To Update");
                Assert.Equal(new DateTime(2021, 4, 23), followUp.First().BudgetDate);
                Assert.Equal(new DateTime(2021, 5, 23), followUp[1].BudgetDate);
            }
        }

        [Fact]
        public void UpdateFollowEntriesForRepetitionChange_Future()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry To Update",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().ToList();
                var entryToChange = new BudgetEntry()
                {
                    Amount = 15,
                    BudgetDate = list[3].BudgetDate,
                    Created = list[3].Created,
                    Description = "Updated Test Entry To Monthly",
                    Id = list[3].Id,
                    RepetitionType = (int)RepetitionType.monthly,
                    RepitionOfId = list[3].RepitionOfId,
                    Updated = list[3].Updated
                };

                var check = sut.Update(entryToChange, UpdatePattern.future);

                Assert.NotNull(check);
                Assert.Equal("Weekly Test Entry To Update", check.Description);
                Assert.Equal(10, check.Amount);
                Assert.Equal(new DateTime(2021, 4, 13), check.BudgetDate);

                var followUp = sut.GetAll().Where(x => x.RepitionOfId == 1).ToList();
                Assert.True(followUp.Count > 0);

                // old entries and current entry should not be changed
                Assert.Equal(new DateTime(2021, 3, 30), followUp[0].BudgetDate);
                Assert.Equal(10, followUp[0].Amount);
                Assert.Equal("Weekly Test Entry To Update", followUp[0].Description);
                Assert.Equal(new DateTime(2021, 4, 6), followUp[1].BudgetDate);
                Assert.Equal(10, followUp[1].Amount);
                Assert.Equal("Weekly Test Entry To Update", followUp[1].Description);
                Assert.Equal(new DateTime(2021, 4, 13), followUp[2].BudgetDate);
                Assert.Equal(10, followUp[2].Amount);
                Assert.Equal("Weekly Test Entry To Update", followUp[2].Description);

                // future entries should be changed
                Assert.Equal(new DateTime(2021, 5, 20), followUp[3].BudgetDate);
                Assert.Equal("Updated Test Entry To Monthly", followUp[3].Description);
                Assert.Equal(15, followUp[3].Amount);
                Assert.Equal(new DateTime(2021, 6, 20), followUp[4].BudgetDate);
                Assert.Equal("Updated Test Entry To Monthly", followUp[4].Description);
                Assert.Equal(15, followUp[4].Amount);
            }
        }

        [Fact]
        public void UpdateFollowEntriesForRepetitionChange_FutureAndCurrent()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IBudgetRepository budgetRepository = new BudgetRepository(context);
                IBudgetService sut = new BudgetService(budgetRepository);

                var checkEntry = sut.Add(new BudgetEntry()
                {
                    Amount = 10,
                    BudgetDate = new DateTime(2021, 3, 23),
                    Description = $"Weekly Test Entry To Update",
                    RepetitionType = (int)RepetitionType.weekly
                });

                var list = sut.GetAll().ToList();
                var entry3 = list[3];
                var entryToChange = new BudgetEntry()
                {
                    Amount = 15,
                    BudgetDate = entry3.BudgetDate,
                    Created = entry3.Created,
                    Description = "Updated Test Entry To Monthly",
                    Id = entry3.Id,
                    RepetitionType = (int)RepetitionType.monthly,
                    RepitionOfId = entry3.RepitionOfId,
                    Updated = entry3.Updated
                };

                var check = sut.Update(entryToChange, UpdatePattern.currentAndFutur);

                Assert.NotNull(check);
                Assert.Equal("Updated Test Entry To Monthly", check.Description);
                Assert.Equal(15, check.Amount);
                Assert.Equal(new DateTime(2021, 4, 13), check.BudgetDate);

                list = sut.GetAll().ToList();
                Assert.NotNull(list);
                Assert.True(list.Count != 0);

                var entry_0 = list[0];
                Assert.Equal("Weekly Test Entry To Update", entry_0.Description);
                Assert.Equal(10, entry_0.Amount);
                Assert.Equal(new DateTime(2021, 3, 23), entry_0.BudgetDate);

                var entry_3 = list[3];
                Assert.Equal("Updated Test Entry To Monthly", entry_3.Description);
                Assert.Equal(15, entry_3.Amount);
                Assert.Equal(new DateTime(2021, 4, 13), entry_3.BudgetDate);

            }
        }
    }
}