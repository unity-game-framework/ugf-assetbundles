using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileDrawer : DrawerBase
    {
        public string Path { get; private set; }

        public bool DebugDisplay
        {
            get { return m_debugDisplay; }
            set
            {
                m_debugDisplay = value;

                OnDebugDisplayChanged();
            }
        }

        public bool HasData { get { return m_drawerNormal.HasData || m_drawerDebug.HasData; } }

        private readonly AssetBundleFileInfoDrawer m_drawerNormal = new AssetBundleFileInfoDrawer();
        private readonly AssetBundleDrawer m_drawerDebug = new AssetBundleDrawer();
        private bool m_debugDisplay;

        protected override void OnEnable()
        {
            base.OnEnable();

            EditorApplication.playModeStateChanged += OnEditorApplicationOnplayModeStateChanged;

            m_drawerNormal.Enable();
            m_drawerDebug.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EditorApplication.playModeStateChanged -= OnEditorApplicationOnplayModeStateChanged;

            Clear();

            m_drawerNormal.Disable();
            m_drawerDebug.Disable();
        }

        public void Set(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            Path = path;

            Clear();

            if (File.Exists(path))
            {
                m_drawerNormal.Set(path);
            }
        }

        public void Clear()
        {
            m_drawerNormal.Clear();
            m_drawerDebug.Clear();
        }

        public void DrawGUILayout()
        {
            using (new EditorGUI.DisabledScope(true))
            {
                if (DebugDisplay)
                {
                    if (EditorApplication.isPlayingOrWillChangePlaymode)
                    {
                        EditorGUILayout.HelpBox("Previewing AssetBundle debug information unavailable in play mode.", MessageType.Info);
                    }
                    else
                    {
                        m_drawerDebug.DrawGUILayout();
                    }
                }
                else
                {
                    m_drawerNormal.DrawGUILayout();
                }

                if (!File.Exists(Path))
                {
                    EditorGUILayout.HelpBox($"No AssetBundle found at the specified path: '{Path}'.", MessageType.Warning);
                }
            }
        }

        private void OnDebugDisplayChanged()
        {
            Clear();

            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (m_debugDisplay)
                {
                    m_drawerDebug.Set(Path);
                }
                else
                {
                    m_drawerNormal.Set(Path);
                }
            }
        }

        private void OnEditorApplicationOnplayModeStateChanged(PlayModeStateChange change)
        {
            Clear();
        }
    }
}
