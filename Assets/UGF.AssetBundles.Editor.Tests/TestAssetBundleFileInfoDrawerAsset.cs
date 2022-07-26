using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestAssetBundleFileInfoDrawerAsset")]
    public class TestAssetBundleFileInfoDrawerAsset : ScriptableObject
    {
    }

    [CustomEditor(typeof(TestAssetBundleFileInfoDrawerAsset))]
    public class TestAssetBundleFileInfoDrawerAssetEditor : UnityEditor.Editor
    {
        private readonly AssetBundleFileInfoDrawer m_drawer = new AssetBundleFileInfoDrawer();
        private AssetBundleFileInfo m_info;

        private void OnEnable()
        {
            m_drawer.Enable();

            var assetNames = new List<string>();
            var scenePaths = new List<string>();
            var assets = new List<AssetBundleFileInfo.AssetInfo>();
            var dependencies = new List<string>();

            for (int i = 0; i < 200; i++)
            {
                assetNames.Add($"Asset Name {i}");
            }

            for (int i = 0; i < 200; i++)
            {
                scenePaths.Add($"Scene Path {i}");
            }

            for (int i = 0; i < 200; i++)
            {
                assets.Add(new AssetBundleFileInfo.AssetInfo($"Asset {i}", typeof(Object), "Address"));
            }

            for (int i = 0; i < 200; i++)
            {
                dependencies.Add($"Dependency {i}");
            }

            m_info = new AssetBundleFileInfo("Test", 15, assetNames, scenePaths, assets, dependencies, true);
            m_drawer.Set(m_info);
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_drawer.DrawGUILayout();
        }
    }
}
