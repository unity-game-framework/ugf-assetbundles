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

            var assets = new[]
            {
                new AssetBundleFileInfo.AssetInfo("Asset 0", typeof(Object), "Address"),
                new AssetBundleFileInfo.AssetInfo("Asset 1", typeof(Object), "Address"),
                new AssetBundleFileInfo.AssetInfo("Asset 2", typeof(Object), "Address")
            };

            string[] dependencies = new[]
            {
                "Test 0",
                "Test 1",
                "Test 2",
                "Test 3",
                "Test 4"
            };

            m_info = new AssetBundleFileInfo("Test", 15, assets, dependencies, true);
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_drawer.DrawGUILayout(m_info);
        }
    }
}
