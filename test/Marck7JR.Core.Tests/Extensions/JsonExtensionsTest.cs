using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Marck7JR.Core.Extensions
{
    [TestClass]
    public class JsonExtensionsTest
    {
        public TestContext? TestContext { get; set; }

        private class DataSet
        {
            public int? Test { get; set; }
            public int? Value { get; set; }
        }

        private static readonly DataSet dataSet = new()
        {
            Test = 1,
            Value = 2,
        };

        private const string json = "{\"Test\":1,\"Value\":2}";

        private static Action<DataSet?> FromJsonAssert => (@object) =>
        {
            Assert.IsNotNull(@object);
            Assert.IsInstanceOfType(@object, dataSet.GetType());
            Assert.AreEqual(1, @object?.Test);
            Assert.AreEqual(2, @object?.Value);
        };

        private static Action<string?> ToJsonAssert => (@string) =>
        {
            Assert.IsTrue(!string.IsNullOrEmpty(@string));
            Assert.AreEqual(json, @string);
        };

        [TestMethod]
        public void FromJson_IsNotNull()
        {
            var @object = json.FromJson(typeof(DataSet)) as DataSet;

            FromJsonAssert(@object);
        }

        [TestMethod]
        public async Task FromJsonAsync_IsNotNull()
        {
            var @object = await json.FromJsonAsync(typeof(DataSet)) as DataSet;

            FromJsonAssert(@object);
        }

        [TestMethod]
        public void FromJsonGeneric_IsNotNull()
        {
            var @object = json.FromJson<DataSet>();

            FromJsonAssert(@object);
        }

        [TestMethod]
        public async Task FromJsonGenericAsync_IsNotNull()
        {
            var @object = await json.FromJsonAsync<DataSet>();

            FromJsonAssert(@object);
        }

        [TestMethod]
        public void ToJson_IsNotNullOrEmpty()
        {
            var json = dataSet.ToJson(dataSet.GetType());

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public async Task ToJsonAsync_IsNotNullOrEmpty()
        {
            var json = await dataSet.ToJsonAsync(dataSet.GetType());

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public void ToJsonGeneric_IsNotNullOrEmpty()
        {
            var json = dataSet.ToJson();

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public async Task ToJsonGenericAsync_IsNotNullOrEmpty()
        {
            var json = await dataSet.ToJsonAsync();

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }
    }
}
