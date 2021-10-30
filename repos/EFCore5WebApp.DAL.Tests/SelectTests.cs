using NUnit.Framework;
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
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this._configuration, "connection"));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}