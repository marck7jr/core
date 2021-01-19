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

        private readonly DataSet _dataSet = new()
        {
            Test = 1,
            Value = 2,
        };

        private const string _json = "{\"Test\":1,\"Value\":2}";

        private Action<DataSet?> FromJsonAssert => (@object) =>
        {
            Assert.IsNotNull(@object);
            Assert.IsInstanceOfType(@object, _dataSet.GetType());
            Assert.AreEqual(1, @object?.Test);
            Assert.AreEqual(2, @object?.Value);
        };

        private Action<string?> ToJsonAssert => (json) =>
        {
            Assert.IsTrue(!string.IsNullOrEmpty(json));
            Assert.AreEqual(_json, json);
        };

        [TestMethod]
        public void FromJson_IsNotNull()
        {
            var @object = _json.FromJson(typeof(DataSet)) as DataSet;

            FromJsonAssert(@object);
        }

        [TestMethod]
        public async Task FromJsonAsync_IsNotNull()
        {
            var @object = await _json.FromJsonAsync(typeof(DataSet)) as DataSet;

            FromJsonAssert(@object);
        }

        [TestMethod]
        public void FromJsonGeneric_IsNotNull()
        {
            var @object = _json.FromJson<DataSet>();

            FromJsonAssert(@object);
        }

        [TestMethod]
        public async Task FromJsonGenericAsync_IsNotNull()
        {
            var @object = await _json.FromJsonAsync<DataSet>();

            FromJsonAssert(@object);
        }

        [TestMethod]
        public void ToJson_IsNotNullOrEmpty()
        {
            var json = _dataSet.ToJson(_dataSet.GetType());

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public async Task ToJsonAsync_IsNotNullOrEmpty()
        {
            var json = await _dataSet.ToJsonAsync(_dataSet.GetType());

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public void ToJsonGeneric_IsNotNullOrEmpty()
        {
            var json = _dataSet.ToJson();

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }

        [TestMethod]
        public async Task ToJsonGenericAsync_IsNotNullOrEmpty()
        {
            var json = await _dataSet.ToJsonAsync();

            ToJsonAssert(json);

            TestContext?.WriteLine(json);
        }
    }
}
