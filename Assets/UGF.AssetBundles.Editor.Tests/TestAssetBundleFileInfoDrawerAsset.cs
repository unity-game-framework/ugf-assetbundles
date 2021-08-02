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
            m_info = new AssetBundleFileInfo("Test", 15, true);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_drawer.DrawGUILayout(m_info);
        }
    }
}
