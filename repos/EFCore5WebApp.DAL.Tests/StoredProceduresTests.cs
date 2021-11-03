using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore5WebApp.DAL.Tests
{
    [TestFixture]
    public class StoredProceduresTests
    {
        private AppDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Data Source=DESKTOP-M35SRPF;Initial Catalog=EFCore5App;Integrated Security=True;").Options);
        }

        [Test]
        public void GetPersonByStateInterpolated()
        {
            string state = "IL";
            var persons = _context.Persons.FromSqlInterpolated($"GetPersonByState {state}").ToList();
            Assert.AreEqual(2, persons.Count);
        }

        [Test]
        public void GetPersonByStateRaw()
        {
            string state = "IL";
            var persons = _context.Persons.FromSqlRaw($"GetPersonByState @p0", new[] { state }).ToList();
            Assert.AreEqual(2, persons.Count);
        }

        [Test]
        public void AddLookUpItemInterpolated()
        {
            string code = "CAN";
            string description = "Canada";
            LookUpType lookUpType = LookUpType.Country;
            _context.Database.ExecuteSqlInterpolated($"AddLookUpItem {code}, {description}, {lookUpType}");

            var addedItem = _context.LookUps.Single(x => x.Code == "CAN");
            Assert.IsNotNull(addedItem);

            _context.LookUps.Remove(addedItem);
            _context.SaveChanges();
        }

        [Test]
        public void AddLookUpItemRaw()
        {
            string code = "MEX";
            string description = "Mexico";
            LookUpType lookUpType = LookUpType.Country;

            _context.Database.ExecuteSqlRaw($"AddLookUpItem @p0, @p1, @p2", new object[] { code, description, lookUpType });

            var addedItem = _context.LookUps.Single(x => x.Code == "MEX");
            Assert.IsNotNull(addedItem);

            _context.LookUps.Remove(addedItem);
            _context.SaveChanges();
        }
    }
}
