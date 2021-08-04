using System;
using System.IO;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Tasks;
using UnityEngine;

namespace UGF.AssetBundles.Runtime
{
    public class AssetBundleHandler
    {
        public string Path { get; }
        public AssetBundle AssetBundle { get { return m_assetBundle ? m_assetBundle : throw new InvalidOperationException("AssetBundle not loaded."); } }
        public bool HasAssetBundle { get { return m_assetBundle != null; } }

        private AssetBundle m_assetBundle;

        public AssetBundleHandler(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            Path = path;
        }

        public void Load()
        {
            if (HasAssetBundle) throw new InvalidOperationException("AssetBundle already loaded.");
            if (!File.Exists(Path)) throw new ArgumentException($"AssetBundle not found at the specified path: '{Path}'.");

            m_assetBundle = AssetBundle.LoadFromFile(Path);
        }

        public async Task LoadAsync()
        {
            if (HasAssetBundle) throw new InvalidOperationException("AssetBundle already loaded.");
            if (!File.Exists(Path)) throw new ArgumentException($"AssetBundle not found at the specified path: '{Path}'.");

            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(Path);

            await request.WaitAsync();

            m_assetBundle = request.assetBundle;
        }

        public void Unload(bool unloadAllLoadedObjects = true)
        {
            AssetBundle.Unload(unloadAllLoadedObjects);

            m_assetBundle = null;
        }

        public async Task UnloadAsync(bool unloadAllLoadedObjects = true)
        {
            await AssetBundle.UnloadAsync(unloadAllLoadedObjects).WaitAsync();

            m_assetBundle = null;
        }
    }
}
