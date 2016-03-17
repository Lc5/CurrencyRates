﻿using Moq;
using System.Data.Entity;
using System.Linq;

namespace CurrencyRates.Tests
{
    internal static class TestUtils
    {
        public static Mock<DbSet<TEntity>> BuildDbSetMock<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(items.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(items.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            return mockSet;
        }
    }
}