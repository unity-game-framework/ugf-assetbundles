using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestAssetBundleFileDrawerAsset")]
    public class TestAssetBundleFileDrawerAsset : ScriptableObject
    {
        [SerializeField] private string m_path = "Assets/StreamingAssets/AssetBundles/AssetBundle_1";

        public string Path { get { return m_path; } set { m_path = value; } }
    }

    [CustomEditor(typeof(TestAssetBundleFileDrawerAsset))]
    public class TestAssetBundleFileDrawerAssetEditor : UnityEditor.Editor
    {
        private readonly AssetBundleFileDrawer m_drawer = new AssetBundleFileDrawer();
        private SerializedProperty m_propertyPath;

        private void OnEnable()
        {
            m_propertyPath = serializedObject.FindProperty("m_path");

            m_drawer.Enable();
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_drawer.DebugDisplay = EditorGUILayout.Toggle("Debug Display", m_drawer.DebugDisplay);

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Update"))
                {
                    m_drawer.Set(m_propertyPath.stringValue);
                }
            }

            m_drawer.DrawGUILayout();
        }
    }
}
