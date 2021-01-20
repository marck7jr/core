using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities;

namespace Marck7JR.Core.Extensions
{
    [TestClass]
    public class YamlExtensionsTest
    {
        
        private class DataSet
        {
            public string? Text { get; set; }
            public int? Number { get; set; }
        }

        private const string yaml = "Text: Test\r\nNumber: 2021\r\n";

        private static readonly DataSet dataSet = new()
        {
            Text = "Test",
            Number = 2021,
        };

        private static Action<string?> ToYamlAssert => (@string) =>
        {
            Assert.IsTrue(!string.IsNullOrEmpty(@string));
            Assert.AreEqual(@string, yaml);
        };

        private static Action<DataSet?> FromYamlAssert => (@object) =>
        {
            Assert.IsNotNull(@object);
            Assert.IsInstanceOfType(@object, dataSet.GetType());
            Assert.AreEqual("Test", @object?.Text);
            Assert.AreEqual(2021, @object?.Number);
        };

        public TestContext? TestContext { get; set; }

        [TestMethod]
        public void FromYaml_IsNotNull()
        {
            var @object = yaml.FromYaml(typeof(DataSet)) as DataSet;

            FromYamlAssert(@object);
        }

        [TestMethod]
        public void FromYamlGeneric_IsNotNull()
        {
            var @object = yaml.FromYaml<DataSet>();

            FromYamlAssert(@object);
        }

        [TestMethod]
        public void ToYaml_IsNotNullOrEmpty()
        {
            var yaml = dataSet.ToYaml(typeof(DataSet));

            ToYamlAssert(yaml);

            TestContext?.WriteLine(yaml);
        }

        [TestMethod]
        public void ToYamlGeneric_IsNotNullOrEmpty()
        {
            var yaml = dataSet.ToYaml<DataSet>();

            ToYamlAssert(yaml);

            TestContext?.WriteLine(yaml);
        }
    }
}
