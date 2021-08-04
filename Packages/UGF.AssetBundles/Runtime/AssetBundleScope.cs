using System;

namespace UGF.AssetBundles.Runtime
{
    public readonly struct AssetBundleScope : IDisposable
    {
        public AssetBundleHandler Handler { get; }

        public AssetBundleScope(string path, bool load = true)
        {
            Handler = new AssetBundleHandler(path);

            if (load)
            {
                Handler.Load();
            }
        }

        public AssetBundleScope(AssetBundleHandler handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void Dispose()
        {
            Handler.Unload();
        }
    }
}
