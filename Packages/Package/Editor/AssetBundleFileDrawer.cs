using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Dropdown;
using UnityEditor;
using UnityEngine;

namespace UGF.AssetBundles.Editor
{
    public class AssetBundleFileDrawer : DrawerBase
    {
        public string Path { get; private set; }

        public bool DisplayDebug
        {
            get { return m_displayDebug; }
            set
            {
                if (m_displayDebug != value)
                {
                    m_displayDebug = value;

                    OnDebugDisplayOrPathChanged();
                }
            }
        }

        public bool DisplayTitlebar { get; set; } = true;
        public bool HasData { get { return m_drawerNormal.HasData || m_drawerDebug.HasData; } }

        private readonly AssetBundleFileInfoDrawer m_drawerNormal = new AssetBundleFileInfoDrawer();
        private readonly AssetBundleDrawer m_drawerDebug = new AssetBundleDrawer();
        private bool m_displayDebug;
        private bool m_foldout;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ContentTitlebarNormal { get; } = new GUIContent("AssetBundle Information");
            public GUIContent ContentTitlebarDebug { get; } = new GUIContent("AssetBundle Information (Debug)");
            public GUIContent ContentMenu { get; } = new GUIContent(EditorGUIUtility.FindTexture("_Menu"));
            public GUIContent ContentMenuRefresh { get; } = new GUIContent("Refresh");
            public GUIContent ContentMenuClear { get; } = new GUIContent("Clear");
            public GUIContent ContentMenuDebug { get; } = new GUIContent("Debug");
            public GUIStyle StyleButton { get; } = new GUIStyle("IconButton");
        }

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

            OnDebugDisplayOrPathChanged();
        }

        public void Clear()
        {
            m_drawerNormal.Clear();
            m_drawerDebug.Clear();
        }

        public void Refresh()
        {
            if (!string.IsNullOrEmpty(Path))
            {
                Set(Path);
            }
        }

        public void DrawGUILayout()
        {
            m_styles ??= new Styles();

            if (DisplayTitlebar)
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
                {
                    GUIContent contentTitlebar = DisplayDebug ? m_styles.ContentTitlebarDebug : m_styles.ContentTitlebarNormal;

                    GUILayout.Label(contentTitlebar, EditorStyles.boldLabel);

                    GUILayout.FlexibleSpace();

                    if (DropdownEditorGUIUtility.DropdownButton(GUIContent.none, m_styles.ContentMenu, out Rect dropdownPosition, FocusType.Keyboard, m_styles.StyleButton, GUILayout.Width(EditorGUIUtility.singleLineHeight)))
                    {
                        OnMenuOpen(dropdownPosition);
                    }
                }
            }

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorGUILayout.HelpBox("Previewing AssetBundle information unavailable in play mode.", MessageType.Info);
            }
            else
            {
                if (DisplayDebug)
                {
                    m_drawerDebug.DrawGUILayout();
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

        private void OnDebugDisplayOrPathChanged()
        {
            Clear();

            if (!string.IsNullOrEmpty(Path) && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (m_displayDebug)
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

        private void OnMenuOpen(Rect position)
        {
            var menu = new GenericMenu();

            menu.AddItem(m_styles.ContentMenuRefresh, false, OnMenuRefresh);
            menu.AddItem(m_styles.ContentMenuClear, false, OnMenuClear);
            menu.AddSeparator(string.Empty);
            menu.AddItem(m_styles.ContentMenuDebug, DisplayDebug, OnMenuDebug);

            menu.DropDown(position);
        }

        private void OnMenuRefresh()
        {
            Refresh();
        }

        private void OnMenuClear()
        {
            Clear();
        }

        private void OnMenuDebug()
        {
            DisplayDebug = !DisplayDebug;
        }
    }
}
