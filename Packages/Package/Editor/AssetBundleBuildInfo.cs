using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleBuildInfo
    {
        public string Name { get; }
        public string VariantName { get; }
        public IReadOnlyDictionary<string, string> Assets { get; }

        private readonly Dictionary<string, string> m_assets = new Dictionary<string, string>();

        public AssetBundleBuildInfo(string name) : this(name, string.Empty)
        {
        }

        public AssetBundleBuildInfo(string name, string variantName)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            VariantName = variantName ?? throw new ArgumentNullException(nameof(variantName));
            Assets = new ReadOnlyDictionary<string, string>(m_assets);
        }

        public void AddAsset(string address, string assetPath)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Value cannot be null or empty.", nameof(address));
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            m_assets.Add(address, assetPath);
        }

        public bool RemoveAsset(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Value cannot be null or empty.", nameof(address));

            return m_assets.Remove(address);
        }

        public void ClearAssets()
        {
            m_assets.Clear();
        }

        public Dictionary<string, string>.Enumerator GetEnumerator()
        {
            return m_assets.GetEnumerator();
        }
    }
}
