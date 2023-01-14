using EShopping.Core.Domain.Entities;
using EShopping.Core.Domain.Enums;
using EShopping.Core.Domain.ValueObjects;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.UnitTests.Domain.Entities
{
    public class OrderShould
    {
        [Test]
        public void Test_InstantiatingOrder_WithEmptyOrderItems_ExpectsBusinessRuleBrokenException()
        {
            // act
            TestDelegate testDelegate = () => new Order("IRAN", new OrderItem[] { });


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);
        }


        [Test]
        public void Test_OrderItemsProperty_AddingOrderItemToReadOnlyCollection_ExpectsNotSupportedException()
        {
            // arrange
            var order = new Order("IRAN", new OrderItem[] { new OrderItem(1, new Price(1, MoneyUnit.Dollar)) });


            // act
            TestDelegate testDelegate = () => order.OrderItems.Add(new OrderItem(1, new Price(1, MoneyUnit.Dollar)));


            // assert
            var ex = Assert.Throws<NotSupportedException>(testDelegate);
        }


        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_10000_Dollar_ExpectsBusinessRuleBrokenException()
        {
            // arrange

            var orderItem1 = new OrderItem(1, new Price(5000, MoneyUnit.Dollar));

            var orderItem2 = new OrderItem(2, new Price(6000, MoneyUnit.Dollar));

            // act
            TestDelegate testDelegate = () =>
            {
                new Order("IRAN", new OrderItem[] { orderItem1, orderItem2 });
            };


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("maximum price"));
        }


        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_9000_Euro_ExpectsBusinessRuleBrokenException()
        {
            // arrange

            var orderItem1 = new OrderItem(1, new Price(5000, MoneyUnit.Dollar));

            var orderItem2 = new OrderItem(2, new Price(6000, MoneyUnit.Dollar));

            // act
            TestDelegate testDelegate = () =>
            {
                new Order("IRAN", new OrderItem[] { orderItem1, orderItem2 });
            };


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("maximum price"));
        }

    }
}
