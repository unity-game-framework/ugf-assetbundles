using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace UGF.AssetBundles.Runtime.Tests
{
    public class TestAssetBundleScope
    {
        [Test]
        public void Scope()
        {
            using (var scope = new AssetBundleScope("Assets/StreamingAssets/AssetBundles/AssetBundle_2"))
            {
                Assert.True(scope.Handler.HasAssetBundle);
                Assert.NotNull(scope.Handler.AssetBundle);
                Assert.True(AssetBundle.GetAllLoadedAssetBundles().Any(x => x.name.StartsWith("8b70", StringComparison.OrdinalIgnoreCase)));
            }

            Assert.False(AssetBundle.GetAllLoadedAssetBundles().Any(x => x.name.StartsWith("8b70", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
