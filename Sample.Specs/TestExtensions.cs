using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Sample.Specs
{
    public static class TestExtensions
    {
        public static void ShouldBe<T>(this T anObj, T value)
        {
            Assert.AreEqual(value, anObj);
        }

        public static void ShouldNotBe<T>(this T valueToTest, T expected)
        {
            Assert.AreNotEqual(expected, valueToTest);
        }

        public static void ShouldBeGreaterThan(this double value, double expected)
        {
            Assert.Greater(value, expected);
        }

        public static void ShouldBeLessThan(this double value, double expected)
        {
            Assert.Less(value, expected);
        }

        public static void ShouldBeLessThan(this int value, int expected)
        {
            Assert.Less(value, expected);
        }

        public static void ShouldNotBeNull<T>(this T anObj)
        {
            Assert.IsNotNull(anObj);
        }

        public static void ShouldBeNull<T>(this T valueToTest)
        {
            Assert.IsNull(valueToTest);
        }

        public static void ShouldBeSameAs<T>(this T valueToTest, T expected)
        {
            Assert.AreSame(valueToTest, expected);
        }

        public static void ShouldNotBeSameAs<T>(this T valueToTest, T expected)
        {
            Assert.AreNotSame(valueToTest, expected);
        }

        public static void ShouldBeTrue(this bool valueToTest)
        {
            Assert.IsTrue(valueToTest);
        }

        public static void ShouldBeFalse(this bool valueToTest)
        {
            Assert.IsFalse(valueToTest);
        }

        public static void ShouldBeInstanceOfType<T>(this object valueToTest)
        {
            Assert.IsInstanceOfType(typeof(T), valueToTest);
        }

        public static void ShouldNotBeInstanceOfType<T>(this object valueToTest)
        {
            Assert.IsNotInstanceOfType(typeof(T), valueToTest);
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> list)
        {
            Assert.AreEqual(0, list.Count());
        }

        public static void ShouldContain(this string str, string expectedStr)
        {
            Assert.IsTrue(str.Contains(expectedStr), string.Format("string \"{0}\" should contained \"{1}\"", str, expectedStr));
        }

        public static void ShouldContain<T>(this IEnumerable<T> list, T expectedObject)
        {
            if (!list.Contains(expectedObject))
            {
                Assert.Fail("Expected object was not in list");
            }
        }

        public static void ShouldHave<T>(this IEnumerable<T> list, int expectedCount)
        {
            Assert.AreEqual(expectedCount, list.Count());
        }

        public static void ShouldHaveMoreThan<T>(this IEnumerable<T> list, int expectedCount)
        {
            Assert.Greater(list.Count(), expectedCount);
        }

        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
    }
}