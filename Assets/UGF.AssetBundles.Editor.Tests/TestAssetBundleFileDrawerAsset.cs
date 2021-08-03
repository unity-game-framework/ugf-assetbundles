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

            m_drawer.DisplayDebug = EditorGUILayout.Toggle("DisplayDebug", m_drawer.DisplayDebug);
            m_drawer.DisplayTitlebar = EditorGUILayout.Toggle("DisplayTitlebar", m_drawer.DisplayTitlebar);
            m_drawer.DisplayMenu = EditorGUILayout.Toggle("DisplayMenu", m_drawer.DisplayMenu);
            m_drawer.DisplayMenuRefresh = EditorGUILayout.Toggle("DisplayMenuRefresh", m_drawer.DisplayMenuRefresh);
            m_drawer.DisplayMenuClear = EditorGUILayout.Toggle("DisplayMenuClear", m_drawer.DisplayMenuClear);
            m_drawer.DisplayMenuDebug = EditorGUILayout.Toggle("DisplayMenuDebug", m_drawer.DisplayMenuDebug);

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
