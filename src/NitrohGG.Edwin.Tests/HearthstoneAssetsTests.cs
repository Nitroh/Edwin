using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NitrohGG.Edwin.Tests
{
    [TestClass]
    public class HearthstoneAssetsTests
    {
        [TestMethod]
        public void GetFileNames_Test()
        {
            var a = new HearthstoneAssets();
            var fileNames = a.GetFileNames();
            Assert.AreEqual(16, fileNames.Count);
        }
    }
}
