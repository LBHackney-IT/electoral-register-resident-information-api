using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private IDbContextTransaction _transaction;
        protected ElectoralRegisterContext ElectoralRegisterContext { get; private set; }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(ConnectionString.TestDatabase());
            ElectoralRegisterContext = new ElectoralRegisterContext(builder.Options);

            ElectoralRegisterContext.Database.EnsureCreated();
            _transaction = ElectoralRegisterContext.Database.BeginTransaction();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
