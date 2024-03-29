﻿using System;
using System.Collections.Generic;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileInfo
    {
        public string Name { get; }
        public uint Crc { get; }
        public IReadOnlyList<string> AssetNames { get; }
        public IReadOnlyList<string> ScenePaths { get; }
        public IReadOnlyList<AssetInfo> Assets { get; }
        public IReadOnlyList<string> Dependencies { get; }
        public bool IsStreamedSceneAssetBundle { get; }

        public class AssetInfo
        {
            public string Name { get; }
            public Type Type { get; }
            public string Address { get; }

            public AssetInfo(string name, Type type, string address)
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

                Name = name;
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Address = address ?? string.Empty;
            }
        }

        public AssetBundleFileInfo(string name, uint crc, bool isStreamedSceneAssetBundle) : this(name, crc, Array.Empty<string>(), Array.Empty<string>(), Array.Empty<AssetInfo>(), Array.Empty<string>(), isStreamedSceneAssetBundle)
        {
        }

        public AssetBundleFileInfo(string name, uint crc, IReadOnlyList<string> assetNames, IReadOnlyList<string> scenePaths, IReadOnlyList<AssetInfo> assets, IReadOnlyList<string> dependencies, bool isStreamedSceneAssetBundle)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            Crc = crc;
            AssetNames = assetNames ?? throw new ArgumentNullException(nameof(assetNames));
            ScenePaths = scenePaths ?? throw new ArgumentNullException(nameof(scenePaths));
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            IsStreamedSceneAssetBundle = isStreamedSceneAssetBundle;
        }
    }
}
