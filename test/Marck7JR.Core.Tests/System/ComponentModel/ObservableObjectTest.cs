using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Marck7JR.Core.Tests.System.ComponentModel
{
    [TestClass]
    public class ObservableObjectTest
    {
        private class Arrange : ObservableObject
        {
            private const string CallbackMessageFormat = "The callback for '{0}' was registered with value -> {1}.";

            private int value1;

            public Arrange(int? value1 = null) : base()
            {
                Value1 = value1 ?? default;
            }

            public int Value1
            {
                get => GetValue(ref value1);
                set => SetValue(ref value1, value, () =>
                {
                    var message = string.Format(CallbackMessageFormat, nameof(Value1), Value1);

                    Debug.WriteLine(message);
                });
            }
        }

        [TestMethod]
        [DataRow(2021)]
        public void InitializeComponent_AreEqual(int value1)
        {
            Arrange arrange = new(value1);

            arrange.InitializeComponent();

            Assert.AreEqual(value1, arrange.Value1);
        }

        [TestMethod]
        [DataRow(2021)]
        public async Task InitializeComponentAsync_AreEqual(int value1)
        {
            Arrange arrange = new(value1);

            var boolean = await arrange.InitializeComponentAsync();

            Assert.IsTrue(boolean);
            Assert.AreEqual(value1, arrange.Value1);
        }
    }
}
