using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class EnumerableExtensionTests
    {
        private IEnumerable<int> _enumerableEmpty;
        private IEnumerable<int> _enumerableTwenty;
        private IEnumerable<int> _enumerableHundred;

        [TestInitialize]
        public void TestInit()
        {
            _enumerableEmpty = Enumerable.Empty<int>();
            _enumerableTwenty = new int[20];
            _enumerableHundred = new int[100];
        }

        [TestMethod]
        public void Enumerable_HasAtLeast_ShouldBeZero()
        {
            Assert.IsTrue(_enumerableEmpty.HasAtLeast(0));
        }

        [TestMethod]
        public void Enumerable_HasAtLeast_ShouldNotBeTwenty()
        {
            Assert.IsFalse(_enumerableEmpty.HasAtLeast(20));
        }

        [TestMethod]
        public void Enumerable_HasAtLeast_ShouldBeAtLeastTwenty()
        {
            Assert.IsTrue(_enumerableHundred.HasAtLeast(20));
        }

        [TestMethod]
        public void Enumerable_HasAtLeast_ShouldBeAtLeastHundred()
        {
            Assert.IsTrue(_enumerableHundred.HasAtLeast(100));
        }

        [TestMethod]
        public void Enumerable_HasAtLeast_ShouldNotBeAtLeastTwoHundred()
        {
            Assert.IsFalse(_enumerableHundred.HasAtLeast(200));
        }

        [TestMethod]
        public void Enumerable_HasAtMost_ShouldBeZero()
        {
            Assert.IsTrue(_enumerableEmpty.HasAtMost(0));
        }

        [TestMethod]
        public void Enumerable_HasAtMost_ShouldNotBeZero()
        {
            Assert.IsFalse(_enumerableTwenty.HasAtMost(0));
        }

        [TestMethod]
        public void Enumerable_HasAtMost_ShouldBeAtMostThirty()
        {
            Assert.IsTrue(_enumerableEmpty.HasAtMost(30));
        }

        [TestMethod]
        public void Enumerable_HasAtMost_ShouldNotBeAtMostThirty()
        {
            Assert.IsFalse(_enumerableHundred.HasAtMost(30));
        }
    }
}
