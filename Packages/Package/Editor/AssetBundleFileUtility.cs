using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
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
                string name = assetBundle.name;
                var assets = new List<AssetBundleFileInfo.AssetInfo>();
                var dependencies = new List<string>();
                bool isStreamedSceneAssetBundle = assetBundle.isStreamedSceneAssetBundle;
                long size = Profiler.GetRuntimeMemorySizeLong(assetBundle);

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

                BuildPipeline.GetCRCForAssetBundle(path, out uint crc);

                return new AssetBundleFileInfo(name, crc, assets, dependencies, isStreamedSceneAssetBundle, size);
            }
            finally
            {
                assetBundle.Unload(true);
            }
        }
    }
}
