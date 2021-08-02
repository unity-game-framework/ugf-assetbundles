using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    public static class AssetBundleBuildUtility
    {
        public static AssetBundleManifest Build(IReadOnlyList<AssetBundleBuildInfo> assetBundles, string outputPath, BuildTarget target, BuildAssetBundleOptions options = BuildAssetBundleOptions.None)
        {
            if (assetBundles == null) throw new ArgumentNullException(nameof(assetBundles));

            AssetBundleBuild[] builds = GetAssetBundleBuilds(assetBundles);
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, options, target);

            return manifest;
        }

        public static AssetBundleBuild[] GetAssetBundleBuilds(IReadOnlyList<AssetBundleBuildInfo> assetBundles)
        {
            if (assetBundles == null) throw new ArgumentNullException(nameof(assetBundles));

            var builds = new AssetBundleBuild[assetBundles.Count];

            for (int i = 0; i < assetBundles.Count; i++)
            {
                AssetBundleBuildInfo info = assetBundles[i];

                var build = new AssetBundleBuild
                {
                    assetBundleName = info.Name,
                    assetBundleVariant = info.VariantName,
                    addressableNames = new string[info.Assets.Count],
                    assetNames = new string[info.Assets.Count],
                };

                int index = 0;

                foreach (KeyValuePair<string, string> pair in info)
                {
                    build.addressableNames[index] = pair.Key;
                    build.assetNames[index] = pair.Value;

                    index++;
                }

                builds[i] = build;
            }

            return builds;
        }
    }
}
