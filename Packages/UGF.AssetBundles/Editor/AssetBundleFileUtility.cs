using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.AssetBundles.Editor
{
    public static class AssetBundleFileUtility
    {
        public static AssetBundleFileInfo Load(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);

            try
            {
                foreach (string assetName in assetBundle.GetAllAssetNames())
                {
                    assetBundle.LoadAsset(assetName);
                }

                BuildPipeline.GetCRCForAssetBundle(path, out uint crc);

                AssetBundleFileInfo info = Create(assetBundle, crc);

                return info;
            }
            finally
            {
                assetBundle.Unload(true);
            }
        }

        public static AssetBundleFileInfo Create(AssetBundle assetBundle, uint crc = 0)
        {
            if (assetBundle == null) throw new ArgumentNullException(nameof(assetBundle));

            string name = assetBundle.name;
            var assetNames = new List<string>();
            var scenePaths = new List<string>();
            var assets = new List<AssetBundleFileInfo.AssetInfo>();
            var dependencies = new List<string>();
            bool isStreamedSceneAssetBundle = assetBundle.isStreamedSceneAssetBundle;

            assetNames.AddRange(assetBundle.GetAllAssetNames());
            scenePaths.AddRange(assetBundle.GetAllScenePaths());

            var serializedObject = new SerializedObject(assetBundle);
            SerializedProperty propertyPreloadTable = serializedObject.FindProperty("m_PreloadTable");
            SerializedProperty propertyContainer = serializedObject.FindProperty("m_Container");
            SerializedProperty propertyDependencies = serializedObject.FindProperty("m_Dependencies");

            var addresses = new Dictionary<int, string>();

            for (int i = 0; i < propertyContainer.arraySize; i++)
            {
                SerializedProperty propertyElement = propertyContainer.GetArrayElementAtIndex(i);
                SerializedProperty propertyFirst = propertyElement.FindPropertyRelative("first");
                SerializedProperty propertySecond = propertyElement.FindPropertyRelative("second");
                SerializedProperty propertyAsset = propertySecond.FindPropertyRelative("asset");

                string address = propertyFirst.stringValue;
                int instanceId = propertyAsset.objectReferenceInstanceIDValue;

                if (instanceId != 0)
                {
                    addresses[instanceId] = address;
                }
            }

            for (int i = 0; i < propertyPreloadTable.arraySize; i++)
            {
                SerializedProperty propertyElement = propertyPreloadTable.GetArrayElementAtIndex(i);
                Object asset = propertyElement.objectReferenceValue;
                int instanceId = propertyElement.objectReferenceInstanceIDValue;

                if (asset != null)
                {
                    string assetName = asset.name;
                    Type assetType = asset.GetType();
                    string address = addresses.TryGetValue(instanceId, out string value) ? value : string.Empty;
                    var assetInfo = new AssetBundleFileInfo.AssetInfo(assetName, assetType, address);

                    assets.Add(assetInfo);
                }
            }

            for (int i = 0; i < propertyDependencies.arraySize; i++)
            {
                SerializedProperty propertyElement = propertyDependencies.GetArrayElementAtIndex(i);

                dependencies.Add(propertyElement.stringValue);
            }

            return new AssetBundleFileInfo(name, crc, assetNames, scenePaths, assets, dependencies, isStreamedSceneAssetBundle);
        }
    }
}
