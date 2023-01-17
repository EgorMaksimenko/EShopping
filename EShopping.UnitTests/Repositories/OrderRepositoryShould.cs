using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EShopping.Core.Persistence;
using EShopping.Core.Domain.Entities;
using EShopping.Core.Domain.ValueObjects;
using EShopping.Core.Persistence.Repositories;
using EShopping.Core.Domain.Enums;
using EShopping.Core.Persistence;

namespace EShopping.UnitTests.Repositories
{
    public class OrderRepositoryShould
    {
        private DbContextOptionsBuilder<EShoppingDbContext> _builder;

        private EShoppingDbContext _dbContext;

        private OrderRepository _orderRepository;


        [OneTimeSetUp]
        public void Setup()
        {
            _builder = new DbContextOptionsBuilder<EShoppingDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_OrderRepository_Database");

            _dbContext = new EShoppingDbContext(_builder.Options);

            _orderRepository = new OrderRepository(_dbContext);
        }


        [Test]
        public async Task Test_MethodAdd_TrackingNumberMustNotBeNull_Ok()
        {
            // arrange
            var order = new Order("IRAN", new OrderItem[]
                                    {
                                        new OrderItem (3, new Price(2000, MoneyUnit.Euro))
                                    });

            // act

            _orderRepository.Add(order);

            var actualOrder = await _orderRepository.GetByIdAsync(1);

            // assert
            Assert.IsNotNull(actualOrder);

            Assert.IsNotNull(actualOrder.TrackingNumber);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            _dbContext.Dispose();
        }
    }
}
