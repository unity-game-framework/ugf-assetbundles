using System;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileInfoDrawer : DrawerBase
    {
        public bool HasData { get { return m_drawer.HasEditor; } }
        public bool DisplayAsReadOnly { get; set; }

        private readonly EditorDrawer m_drawer = new EditorDrawer();
        private AssetBundleFileInfoContainer m_container;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Clear();

            m_drawer.Disable();
        }

        public void Set(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            AssetBundleFileInfo info = AssetBundleFileUtility.Load(path);

            Set(info);
        }

        public void Set(AssetBundleFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            Clear();

            m_container = AssetBundleFileInfoContainerUtility.Create(info);

            m_drawer.Set(m_container);
        }

        public void Clear()
        {
            m_drawer.Clear();

            if (m_container != null)
            {
                Object.DestroyImmediate(m_container);

                m_container = null;
            }
        }

        public void DrawGUILayout()
        {
            if (HasData)
            {
                using (new EditorGUI.DisabledScope(DisplayAsReadOnly))
                {
                    m_drawer.DrawGUILayout();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No data specified.", MessageType.Info);
            }
        }
    }
}
