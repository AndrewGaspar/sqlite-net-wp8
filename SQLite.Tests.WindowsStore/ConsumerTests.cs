using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace SQLite.Tests
{
    [TestClass]
    public class ConsumerTests
    {
        [TestMethod]
        public void TestConsumer()
        {
            var consumer = new SQLiteConsumer.DataHolder();
            
            consumer.AddItem("A tree", 12.31m).Wait();

            var items = consumer.GetItems().Result;

            var item = items.First();

            Assert.AreEqual<string>(item.Description, "A tree");
            Assert.AreEqual<decimal>(item.Value, 12.31m);
        }
    }
}
