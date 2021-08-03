﻿using System;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    internal static class AssetBundleFileInfoContainerUtility
    {
        public static AssetBundleFileInfoContainer CreateContainer(AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            var container = ScriptableObject.CreateInstance<AssetBundleFileInfoContainer>();

            container.Name = info.Name;
            container.Crc = info.Crc;
            container.IsStreamedSceneAssetBundle = info.IsStreamedSceneAssetBundle;
            container.Dependencies.AddRange(info.Dependencies);

            for (int i = 0; i < info.Assets.Count; i++)
            {
                AssetBundleFileInfo.AssetInfo assetInfo = info.Assets[i];

                container.Assets.Add(new AssetBundleFileInfoContainer.AssetInfo
                {
                    Name = assetInfo.Name,
                    Type = assetInfo.Type.FullName,
                    Address = assetInfo.Address
                });
            }

            return container;
        }
    }
}
