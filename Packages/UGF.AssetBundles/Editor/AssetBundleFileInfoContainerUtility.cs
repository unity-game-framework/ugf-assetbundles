using System;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    internal static class AssetBundleFileInfoContainerUtility
    {
        public static AssetBundleFileInfoContainer Create(AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            var container = ScriptableObject.CreateInstance<AssetBundleFileInfoContainer>();

            container.Name = info.Name;
            container.Crc = info.Crc;
            container.IsStreamedSceneAssetBundle = info.IsStreamedSceneAssetBundle;
            container.ScenePaths.AddRange(info.ScenePaths);
            container.Dependencies.AddRange(info.Dependencies);

            for (int i = 0; i < info.Assets.Count; i++)
            {
                AssetBundleFileInfo.AssetInfo assetInfo = info.Assets[i];

                container.Assets.Add(new AssetBundleFileInfoContainer.AssetInfo
                {
                    Name = assetInfo.Name,
                    Type = assetInfo.Type.AssemblyQualifiedName,
                    Address = assetInfo.Address
                });
            }

            container.Assets.Sort((a, b) => string.Compare(b.Address, a.Address, StringComparison.Ordinal));

            return container;
        }
    }
}
