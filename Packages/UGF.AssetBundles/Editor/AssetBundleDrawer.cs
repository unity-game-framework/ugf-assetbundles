using System;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleDrawer : DrawerBase
    {
        public bool HasData { get { return m_drawer.HasEditor; } }
        public bool UnloadOnClear { get; set; } = true;

        private readonly EditorDrawer m_drawer = new EditorDrawer();

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

            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);

            Set(assetBundle);
        }

        public void Set(AssetBundle assetBundle)
        {
            if (assetBundle == null) throw new ArgumentNullException(nameof(assetBundle));

            Clear();

            foreach (string assetName in assetBundle.GetAllAssetNames())
            {
                assetBundle.LoadAsset(assetName);
            }

            m_drawer.Set(assetBundle);
        }

        public void Clear()
        {
            if (m_drawer.HasEditor)
            {
                var assetBundle = (AssetBundle)m_drawer.Editor.target;

                m_drawer.Clear();

                if (UnloadOnClear)
                {
                    assetBundle.Unload(true);
                }
            }
        }

        public void DrawGUILayout()
        {
            if (HasData)
            {
                using (new EditorGUI.DisabledScope(true))
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
