using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore5WebApp.DAL.Tests
{
    [TestFixture]
    public class SelectTests
    {
        private AppDbContext _context;
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Data Source=DESKTOP-M35SRPF;Initial Catalog=EFCore5App;Integrated Security=True;").Options);
        }

        [Test]
        public void GetAllPersons()
        {
            IEnumerable<Person> persons = _context.Persons.ToList();
            Assert.AreEqual(2, persons.Count());
        }

        [Test]
        public void PersonsHaveTwoAddresses()
        {
            List<Person> persons = _context.Persons.Include("Addresses").ToList();
            Assert.AreEqual(1, persons[0].Addresses.Count);
            Assert.AreEqual(2, persons[1].Addresses.Count);
        }

        [Test]
        public void HaveLookUpRecords()
        {
            var lookUps = _context.LookUps.ToList();
            var countries = lookUps.Where(x => x.LookUpType == LookUpType.Country).ToList();
            var states = lookUps.Where(x => x.LookUpType == LookUpType.State).ToList();

            Assert.AreEqual(1, countries.Count);
            Assert.AreEqual(51, states.Count);
        }
    }
}