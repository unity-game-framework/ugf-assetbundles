﻿using System;
using System.Collections.Generic;
using System.IO;
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
                    string address = pair.Key.Replace('\\', '/');
                    string path = pair.Value.Replace('\\', '/');

                    build.addressableNames[index] = address;
                    build.assetNames[index] = path;

                    index++;
                }

                builds[i] = build;
            }

            return builds;
        }

        public static bool DeleteManifestFiles(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            string[] paths = Directory.GetFiles(path, "*.manifest", SearchOption.AllDirectories);

            if (paths.Length > 0)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    File.Delete(paths[i]);
                }

                return true;
            }

            return false;
        }
    }
}
