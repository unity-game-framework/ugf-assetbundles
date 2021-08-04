using NUnit.Framework;

namespace UGF.AssetBundles.Runtime.Tests
{
    public class TestAssetBundleHandler
    {
        [Test]
        public void LoadAndUnload()
        {
            var handler = new AssetBundleHandler("Assets/StreamingAssets/AssetBundles/AssetBundle_1");

            handler.Load();

            Assert.True(handler.HasAssetBundle);
            Assert.NotNull(handler.AssetBundle);

            handler.Unload();

            Assert.False(handler.HasAssetBundle);
        }
    }
}
