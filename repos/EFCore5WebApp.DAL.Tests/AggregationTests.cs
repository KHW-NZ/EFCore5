using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore5WebApp.DAL.Tests
{
    [TestFixture]
    public class AggregationTests
    {
        private AppDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Data Source=DESKTOP-M35SRPF;Initial Catalog=EFCore5App;Integrated Security=True;").Options);
        }

        [Test]
        public void CountPersons()
        {
            int personCount = _context.Persons.Count();
            Assert.AreEqual(2, personCount);
        }

        [Test]
        public void CountPersonWithFilter()
        {
            int personCount = _context.Persons.Count(x => x.FirstName == "John" && x.LastName == "Smith");
            Assert.AreEqual(1, personCount);
        }

        [Test]
        public void MinPersonAge()
        {
            var minPersonAge = _context.Persons.Min(x => x.Age);
            Assert.AreEqual(20, minPersonAge);
        }

        [Test]
        public void MaxPersonAge()
        {
            var maxPersonAge = _context.Persons.Max(x => x.Age);
            Assert.Greater(maxPersonAge, 20);
        }

        [Test]
        public void AveragePersonAge()
        {
            var average = _context.Persons.Average(x => x.Age);
            Assert.AreEqual(25, average);
        }

        [Test]
        public void SumPersonAge()
        {
            var sumPersonAge = _context.Persons.Sum(x => x.Age);
            Assert.AreEqual(50, sumPersonAge);
        }

        [Test]
        public void GroupAddressesByState()
        {
            var expectedILAddressesCount = _context.Addresses.Where(x => x.State == "IL").Count();
            var expectedCAAddressesCount = _context.Addresses.Where(x => x.State == "CA").Count();
            var groupedAddresses = (
                from a in _context.Addresses.ToList() 
                group a by a.State into g 
                select new { 
                    State = g.Key, Addresses = g.Select(x => x) 
            }).ToList();

            Assert.AreEqual(expectedILAddressesCount, groupedAddresses.Single(x => x.State == "IL").Addresses.Count());
            Assert.AreEqual(expectedCAAddressesCount, groupedAddresses.Single(x => x.State == "CA").Addresses.Count());
        }

        [Test]
        public void GroupAddressesByStateCount()
        {
            var expectedILAddressesCount = _context.Addresses.Where(x => x.State == "IL").Count();
            var expectedCAAddressesCount = _context.Addresses.Where(x => x.State == "CA").Count();
            var groupedAddresses = (
                from a in _context.Addresses.ToList()
                group a by a.State into g
                select new
                {
                    State=g.Key, Count = g.Count()
                }
            ).ToList();

            Assert.AreEqual(expectedILAddressesCount, groupedAddresses.Single(x => x.State == "IL").Count);
            Assert.AreEqual(expectedCAAddressesCount, groupedAddresses.Single(x => x.State == "CA").Count);
        }

        [Test]
        public void MinAgePerState()
        {
            var expectedILMinAge = 30;
            var expectedCAMinAge = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, MinAge = g.Min(a => a.Age) };

            Assert.AreEqual(expectedILMinAge, groupedAddresses.Single(x => x.State == "IL").MinAge);
            Assert.AreEqual(expectedCAMinAge, groupedAddresses.Single(x => x.State == "CA").MinAge);           
        }

        [Test]
        public void MaxAgePerState()
        {
            var expectedILMaxAge = 30;
            var expectedCAMaxAge = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, MaxAge = g.Max(a => a.Age) };

            Assert.AreEqual(expectedILMaxAge, groupedAddresses.Single(x => x.State == "IL").MaxAge);
            Assert.AreEqual(expectedCAMaxAge, groupedAddresses.Single(x => x.State == "CA").MaxAge);
        }

        [Test]
        public void AverageAgePerState()
        {
            var expectedAvgAgeIL = 30;
            var expectedAvgAgeCA = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, AverageAge = g.Average(x => x.Age) };

            Assert.AreEqual(expectedAvgAgeIL, groupedAddresses.Single(x => x.State == "IL").AverageAge);
            Assert.AreEqual(expectedAvgAgeCA, groupedAddresses.Single(x => x.State == "CA").AverageAge);
        }

        [Test]
        public void SumAgePerState()
        {
            var expectedSumAgeIL = 60;
            var expectedSumAgeCA = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, SumAge = g.Sum(x => x.Age) };

            Assert.AreEqual(expectedSumAgeIL, groupedAddresses.Single(x => x.State == "IL").SumAge);
            Assert.AreEqual(expectedSumAgeCA, groupedAddresses.Single(x => x.State == "CA").SumAge);
        }
    }
}
